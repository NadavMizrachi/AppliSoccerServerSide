using AppliSoccerDatabasing;
using AppliSoccerObjects.ActionResults.LeagueActions;
using AppliSoccerObjects.Modeling;
using AppliSoccerStatisticAPI.ExternalAPISource;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerEngine.Leagues
{
    public class LeaguesManager
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IDataBaseAPI _dataBaseAPI = Database.GetDatabase();
        private static IStatisticAPI _statisticAPI = ExternalStatisticSource.GetStatisticAPI();

        public LeaguesManager()
        {
        }


        public static void PullTeamLeaguesIds()
        {
            PullTeamLeagues().Wait();
        }

        public async Task<GetMainLeagueActionResult> GetMainLeague(string teamId)
        {
            GetMainLeagueActionResult actionResult = new GetMainLeagueActionResult();
            League mainLeague = await _dataBaseAPI.GetMainLeague(teamId);
            if(mainLeague == null)
            {
                actionResult.Status = AppliSoccerObjects.ActionResults.Status.Fail;
                actionResult.FailReason = GetLeagueFailReason.Unknown;
            }
            else
            {
                League mainLeagueFiltered = FilterSubLeaguesNotRelevantForTeam(mainLeague, teamId);
                actionResult.Status = AppliSoccerObjects.ActionResults.Status.Success;
                actionResult.MainLeague = mainLeagueFiltered;
            }
            return actionResult;
        }

        private League FilterSubLeaguesNotRelevantForTeam(League mainLeague, string teamId)
        {
            Team team = _dataBaseAPI.GetTeam(teamId);
            League filteredLeague = new League
            {
                Name = mainLeague.Name,
                LogoUrl = mainLeague.LogoUrl,
                ID = mainLeague.ID,
                Country = mainLeague.Country,
                Table = new LeagueTable
                {
                    SubTables = new List<SubTable>()
                }
            };
            if (mainLeague.Table == null || mainLeague.Table.SubTables == null) return filteredLeague;
            foreach (var subTable in mainLeague.Table.SubTables)
            {
                if(subTable.Rows.Exists( row => row.TeamId.Equals(team.Id)))
                {
                    filteredLeague.Table.SubTables.Add(subTable);
                }
            }
            return filteredLeague;
        }

        public static void StartLoadLeagueTable()
        {
            PullLeaguesTables();
            var hours = 3;
            var hoursInSeconds = 60 * 60 * hours;
            new ScheduleTask(LeaguesManager.PullLeaguesTables, hoursInSeconds);
        }
        private static async Task PullTeamLeagues()
        {
            _logger.Info("Pulling team leagues from API service...");
            // Pull registered teams
            List<Team> registeredTeams = await _dataBaseAPI.GetRegisteredTeams();

            // Key - league id    value - League object
            Dictionary<string, League> leaguesDict = new Dictionary<string, League>();
            // For each registered team, pull his leagues
            foreach (var team in registeredTeams)
            {
                LeaguesInfo leaguesInfo = await _statisticAPI.GetLeaguesInfo(team.ExtTeamId, Seasons.GetCurrentSeason());
                team.ExtMainLeagueId = leaguesInfo.MainLeagueId;
                team.ExtSeconderyCompetitionsIds = leaguesInfo.SecondLeaguesIds;
                // Update this team with the league IDs
                _logger.Info($"Update leagues id of team: {team.Id} [{team.Name}]");
                await _dataBaseAPI.UpdateTeam(team);

                // Insert / Update the leagues to leagues table in DB 
                List<League> leagues = ExrtactLeagues(leaguesInfo);
                foreach (var lg in leagues)
                {
                    if (!leaguesDict.ContainsKey(lg.ID))
                    {
                        leaguesDict.Add(lg.ID, lg);
                    }
                }
            }

            _logger.Info($"Updating leagues details in leagues DB table...");
            var uniqueLeagues = leaguesDict.Values.ToList();
            await _dataBaseAPI.UpdateLeaguesDetails(uniqueLeagues);
        }

        private static List<League> ExrtactLeagues(LeaguesInfo leaguesInfo)
        {
            List<League> output = new List<League>();
            foreach (var leagueDetails in leaguesInfo.LeaguesDetails)
            {
                output.Add(new League
                    {
                        Country = leagueDetails.Country,
                        ID = leagueDetails.ExtId,
                        LogoUrl = leagueDetails.LogoURL,
                        Name = leagueDetails.Name
                    }
                );
            }
            return output;
        }

        private static void PullLeaguesTables()
        {
            HashSet<string> leaguesIdsSet = new HashSet<string>();
            List<Team> registeredTeams = _dataBaseAPI.GetRegisteredTeams().Result;
            foreach (var team in registeredTeams)
            {
                string mainLeagueId = team.ExtMainLeagueId;
                if(mainLeagueId != null && mainLeagueId.Length > 0)
                {
                    leaguesIdsSet.Add(mainLeagueId);
                }
            }

            foreach (var leagueId in leaguesIdsSet)
            {
                try
                {
                    // Pull standing of league id
                    LeagueTableModel leagueTableModel = _statisticAPI.GetLeagueTable(leagueId, Seasons.GetCurrentSeason()).Result;
                    LeagueTable leagueTable = ExtractLeagueTable(leagueTableModel);
                    // update database
                    _dataBaseAPI.UpdateTableRanks(leagueId, leagueTable).Wait();
                }
                catch( Exception ex)
                {
                    _logger.Error($"Error has occurred during trying to update table rank of league: {leagueId}", ex);
                }
                
            }
        }

        private static LeagueTable ExtractLeagueTable(LeagueTableModel leagueTableModel)
        {
            if (leagueTableModel == null) return null;
            LeagueTable leagueTable = new LeagueTable();
            if (leagueTableModel.SubLeagues == null) return leagueTable;
            leagueTable.SubTables = new List<SubTable>();

            string leagueName = leagueTableModel.LeagueName;

            foreach (var subLeague in leagueTableModel.SubLeagues)
            {
                SubTable subTable = new SubTable();
                subTable.Name = leagueName;
                subTable.Description = subLeague.Description;
                subTable.Rows = ExtractRows(subLeague.Rows);
                leagueTable.SubTables.Add(subTable);
            }
            return leagueTable;


        }

        private static  List<TableRow> ExtractRows(List<TableRowModel> rowsModels)
        {
            if (rowsModels == null) return null;
            List<TableRow> tableRows = new List<TableRow>();
            foreach (var rowModel in rowsModels)
            {
                Team team = _dataBaseAPI.GetTeamByExtId(rowModel.TeamExtId);
                TableRow tableRow = new TableRow
                {
                    Form = rowModel.Form,
                    GoalsDiff = rowModel.GoalsDiff,
                    Points = rowModel.Points,
                    Rank = rowModel.Rank,
                    TeamId = team.Id,
                    TeamName = rowModel.TeamName,
                    LogoURL = team.LogoUrl
                };
                tableRows.Add(tableRow);
            }
            return tableRows;
        }
    }
}
