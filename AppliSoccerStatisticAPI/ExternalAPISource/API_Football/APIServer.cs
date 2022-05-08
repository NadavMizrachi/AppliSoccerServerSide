using AppliSoccerObjects.Modeling;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football.EndPointImp;
using AppliSoccerStatisticAPI.RestEngine;
using RestSharp;
using System;
using System.Collections.Generic;
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

    }
}
