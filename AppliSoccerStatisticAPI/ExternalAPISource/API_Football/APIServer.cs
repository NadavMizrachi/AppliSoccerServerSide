using AppliSoccerObjects.Modeling;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football.EndPointImp;
using AppliSoccerStatisticAPI.RestEngine;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football
{
    public class APIServer : IStatisticAPI 
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private RequestExecuter _requestExecuter;
        public APIServer()
        {
            var serverConfiguration = new APIFootbalServerConfiguration();
            _requestExecuter = new RequestExecuter(serverConfiguration);
        }

        public Task<IEnumerable<Team>> GetTeamsTask(IEnumerable<string> countryNames)
        {
            return Task.Run( () => { return GetTeamsFromServer(countryNames); });
        }


        private IEnumerable<Team> GetTeamsFromServer(IEnumerable<string> countryNames)
        {
            List<Team> teams = new List<Team>();
            foreach (var countryName in countryNames)
            {
                teams.AddRange(GetTeamsFromServer(countryName));
            }
            return teams;
        }
        private IEnumerable<Team> GetTeamsFromServer(string countryName)
        {
            try
            {
                _logger.Info($"Try to get teams of country: {countryName} from API server...");
                TeamsEndPoint teamsEndPoint = new TeamsEndPoint();
                teamsEndPoint.SetParameter(TeamsEndPoint.CountryParamName, countryName);
                RestResponse response = _requestExecuter.ExecuteAsync(teamsEndPoint).Result;
                List<TeamInformationsModel> teamsInfos = ResponseDeserializer.DeserializeAsObjectList<TeamInformationsModel>(response);
                return ModelToObjectConverter.ConvertTeams(teamsInfos);
            }catch(Exception ex)
            {
                _logger.Error(ex);
                _logger.Error(ex.StackTrace);
                return new List<Team>();
            }
        }

        public async Task<LeaguesInfo> GetLeaguesInfo(string teamId, string season)
        {
            try
            {
                _logger.Info($"Try to get leagues teams info for team id: {teamId} from API server...");
                LeaguesEndPoint leaguesEndPoint = new LeaguesEndPoint();
                leaguesEndPoint.SetParameter(LeaguesEndPoint.teamIdParamName, teamId);
                leaguesEndPoint.SetParameter(LeaguesEndPoint.seasonParamName, season);
                RestResponse response = await _requestExecuter.ExecuteAsync(leaguesEndPoint);
                List<TeamLeagueResponse> teamsLeagueInfos = ResponseDeserializer.DeserializeAsObjectList<TeamLeagueResponse>(response);
                LeaguesInfo leaguesInfo = ExtractLeaguesInfo(teamsLeagueInfos, season);
                return leaguesInfo;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _logger.Error(ex.StackTrace);
                return null;
            }
        }


        private LeaguesInfo ExtractLeaguesInfo(List<TeamLeagueResponse> teamsLeagueInfos, string season)
        {
            LeaguesInfo leaguesInfo = new LeaguesInfo();
            leaguesInfo.LeaguesDetails = new List<LeagueDetails>();
            leaguesInfo.SecondLeaguesIds = new List<string>();
            foreach (var teamLeagueInfo in teamsLeagueInfos)
            {
                bool isLeagueOnGivenSeason = teamLeagueInfo.Seasons.Select(s => s.Year).ToList().Contains(season);
                // Check if the league 
                if (isLeagueOnGivenSeason)
                {
                    string leagueId = teamLeagueInfo.League.Id;
                    bool isMainLeague = teamLeagueInfo.League.Type.ToLower().Equals("league");
                    if (isMainLeague)
                    {
                        leaguesInfo.MainLeagueId = leagueId;
                    }
                    else
                    {
                        leaguesInfo.SecondLeaguesIds.Add(leagueId);
                    }
                    LeagueDetails leagueDetails = new LeagueDetails
                    {
                        Country = teamLeagueInfo.Country.Name,
                        Name = teamLeagueInfo.League.Name,
                        ExtId = teamLeagueInfo.League.Id,
                        LogoURL = teamLeagueInfo.League.LogoURL,
                        Type = teamLeagueInfo.League.Type
                    };
                    leaguesInfo.LeaguesDetails.Add(leagueDetails);
                }

            }
            return leaguesInfo;
        }

        public async Task<LeagueTableModel> GetLeagueTable(string leagueId, string season)
        {
            try
            {
                _logger.Info($"Try to get league standing  info for league id: {leagueId} season: {season} from API server...");
                StandingEndPoint standingEndPoint = new StandingEndPoint();
                standingEndPoint.SetParameter(StandingEndPoint.leagueIdParamName, leagueId);
                standingEndPoint.SetParameter(LeaguesEndPoint.seasonParamName, season);
                RestResponse response = await _requestExecuter.ExecuteAsync(standingEndPoint);
                List<StandingResponse> leagueStanding = ResponseDeserializer.DeserializeAsObjectList<StandingResponse>(response);
                LeagueTableModel output = ModelToObjectConverter.ConvertLeagueStanding(leagueStanding.First());
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _logger.Error(ex.StackTrace);
                return null;
            }
        }
    }
}
