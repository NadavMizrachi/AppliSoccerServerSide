using AppliSoccerDatabasing;
using AppliSoccerEngine.Exceptions;
using AppliSoccerObjects.Modeling;
using AppliSoccerStatisticAPI.ExternalAPISource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace AppliSoccerEngine.Registration
{
    public class RegistrationManager
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDataBaseAPI _dataBaseAPI = Database.GetDatabase();
        private IStatisticAPI _statisticAPI = ExternalStatisticSource.GetStatisticAPI();

        public RegistrationManager()
        {
            PullTeamsToDatabase();
        }

        private void PullTeamsToDatabase()
        {
            if (MyConfiguration.GetBoolFromAppSetting("pullTeams"))
            {
                PullTeamsAsync(SupportedCountries.GetCountries());
            }
        }

        /// <summary>
        /// Pulls teams from external API and saves it in the database. If there are new teams, they will
        /// be marked as un registered.
        /// </summary>
        /// <param name="countriesNames">List of countries that the teams will be pulled from.</param>
        public void PullTeamsAsync(IEnumerable<string> countriesNames)
        {
            var teamsFromAPIServer = _statisticAPI.GetTeamsTask(countriesNames);
            foreach (var team in teamsFromAPIServer.Result)
            {
                bool teamAlreadyExist = _dataBaseAPI.IsTeamExistTask(team).Result;
                if (teamAlreadyExist)
                {
                    _logger.Debug($"Trying to insert the team: {team.Id} but this team is already exists");
                }
                else
                {
                    _dataBaseAPI.InsertTeam(team);
                }
            }
        }
        
        public async Task<List<TeamDetails>> GetUnregisteredTeams()
        {
            var teams = await _dataBaseAPI.GetUnregistredTeamsTask();
            var teamDetails = await ExtractTeamDetails(teams);
            return teamDetails;
        }

        public async Task<List<TeamDetails>> GetUnregisteredTeams(string country)
        {
            var teams = await _dataBaseAPI.GetUnregistredTeamsTask(country);
            var teamDetails = await ExtractTeamDetails(teams);
            return teamDetails;
        }

        private Task<List<TeamDetails>> ExtractTeamDetails(List<Team> teams)
        {
            return Task.Run(() =>
            {
                List<TeamDetails> teamDetailList = new List<TeamDetails>();
                foreach (var team in teams)
                {
                    teamDetailList.Add(new TeamDetails() { Id = team.Id, Name = team.Name, CountryName = team.CountryName });
                }
                teamDetailList.Sort((obj1, obj2) => obj1.Name.CompareTo(obj2.Name));
                return teamDetailList;
            });
        }

        public IEnumerable<string> GetCountries()
        {
            return SupportedCountries.GetCountries();
        }

        public async Task RegisterTeamTask(string teamId, string adminUsername, string adminPassword)
        {
            // Check if team is unregistered
            if(await _dataBaseAPI.IsRegisteredTeamTask(teamId))
            {
                throw new TeamAlreadyRegisteredException();
            }
            // Validate username and password
            if(await _dataBaseAPI.IsUsernameExistTask(adminPassword))
            {
                throw new UsernameAlreadyExistsException();
            }
            // Register team & user
            User user = UserFactory.CreateAdminUser(teamId, adminUsername, adminPassword);
            await _dataBaseAPI.InsertUser(user);
            await _dataBaseAPI.MarkTeamAsRegister(teamId);
        }
        
    }
}
