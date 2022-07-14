using AppliSoccerObjects.Modeling;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football
{
    public class ModelToObjectConverter
    {
        public static Team ConvertTeam(TeamInformationsModel teamInformations)
        {
            Team team = 
                new Team(teamInformations.Team.CountryName, teamInformations.Team.Name);
            team.ExtTeamId = teamInformations.Team.Id;
            team.LogoUrl = teamInformations.Team.LogoUrl;
            return team;
        }

        public static List<Team> ConvertTeams(List<TeamInformationsModel> teamInformationsModels)
        {
            var teams = new List<Team>();
            teamInformationsModels.ForEach(teamInfo => teams.Add(ModelToObjectConverter.ConvertTeam(teamInfo)));
            return teams;
        }

        public static LeagueTableModel ConvertLeagueStanding(StandingResponse leagueStanding)
        {
            LeagueTableModel table = new LeagueTableModel();
            table.Country = leagueStanding.StandingLeague.Country;
            table.LeagueExtId = leagueStanding.StandingLeague.Id;
            table.LeagueFlagURL = leagueStanding.StandingLeague.FlagURL;
            table.LeagueLogoURL = leagueStanding.StandingLeague.LogoURL;
            table.LeagueName = leagueStanding.StandingLeague.Name;
            table.SubLeagues = new List<SubLeague>();
            foreach (var standingJson in leagueStanding.StandingLeague.Standings)
            {
                List<StandingRow> standingRows = ParseStandingRows((JArray)standingJson);
                SubLeague subLeague = new SubLeague();
                subLeague.Description = standingRows[0].Group;
                subLeague.Rows = ConvertRows(standingRows);
                table.SubLeagues.Add(subLeague);
            }
            return table;
        }

        private static List<StandingRow> ParseStandingRows(JArray standigRowsAsJsonArray)
        {
            List<StandingRow> parsedStandingRows = new List<StandingRow>();
            foreach (var standingRowJson in standigRowsAsJsonArray)
            {
                var standingRow = JsonConvert.DeserializeObject<StandingRow>(standingRowJson.ToString());
                parsedStandingRows.Add(standingRow);
            }
            return parsedStandingRows;
        }

        private static List<TableRowModel> ConvertRows(List<StandingRow> standingRows)
        {
            List<TableRowModel> rowModels = new List<TableRowModel>();
            foreach (var standingRow in standingRows)
            {
                TableRowModel rowModel = new TableRowModel
                {
                    GoalsDiff = standingRow.GoalsDiff,
                    Form = standingRow.Form,
                    Points = standingRow.Points,
                    Rank = standingRow.Rank,
                    TeamExtId = standingRow.Team.id,
                    TeamName = standingRow.Team.Name
                };
                rowModels.Add(rowModel);
            }
            return rowModels;
        }
    }
}
