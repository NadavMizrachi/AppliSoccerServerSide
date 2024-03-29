﻿using AppliSoccerDatabasing;
using AppliSoccerEngine.Exceptions;
using AppliSoccerEngine.TeamMembers;
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
        private static IDataBaseAPI _dataBaseAPI = Database.GetDatabase();
        private static IStatisticAPI _statisticAPI = ExternalStatisticSource.GetStatisticAPI();
        private readonly Object _locker = new object();

        public RegistrationManager()
        {
            //PullTeamsToDatabase();
        }

        public static void PullTeamsToDatabase()
        {
            PullTeamsAsync(SupportedCountries.GetCountries());
        }

        /// <summary>
        /// Pulls teams from external API and saves it in the database. If there are new teams, they will
        /// be marked as un registered.
        /// </summary>
        /// <param name="countriesNames">List of countries that the teams will be pulled from.</param>
        private static void PullTeamsAsync(IEnumerable<string> countriesNames)
        {
            var teamsFromAPIServer = _statisticAPI.GetTeamsTask(countriesNames);
            foreach (var team in teamsFromAPIServer.Result)
            {
                // TODO - When team exist, we need update the detais (no return like implemented here)
                bool teamAlreadyExist = _dataBaseAPI.IsTeamExistTask(team).Result;
                if (teamAlreadyExist)
                {
                    _logger.Debug($"Trying to insert the team: {team.Id} but this team is already exists");
                }
                else
                {
                    _dataBaseAPI.InsertTeamTask(team);
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
                    teamDetailList.Add(
                        new TeamDetails() {
                            Id = team.Id,
                            Name = team.Name, 
                            CountryName = team.CountryName,
                            LogoURL = team.LogoUrl
                        }
                    );
                }
                teamDetailList.Sort((obj1, obj2) => obj1.Name.CompareTo(obj2.Name));
                return teamDetailList;
            });
        }

        public async Task RegisterUser(User user)
        {
            if (!IsValidUserData(user))
            {
                throw new UserWithUnvalidDataException($"User :{user.Username} has invalid data. user.TeamMember={user.TeamMember}"); 
            }

            if (TeamMemberTypeRecognizer.IsCoach(user.TeamMember))
            {
                string teamId = user.TeamMember.TeamId;
                if(await _dataBaseAPI.IsExistCoach(teamId))
                {
                    throw new CoachAlreadyExistsException($"Coach already exist for team: {user.TeamMember.TeamId}");
                }
            }
            
            if (_dataBaseAPI.IsUsernameExistTask(user.Username).Result)
            {
                throw new UsernameAlreadyExistsException();
            }
            user.Password = Passwords.HashPassword(user.Password);
            await _dataBaseAPI.InsertUserTask(user);
        }

        private bool IsValidUserData(User user)
        {
            return (user != null) &&
                    (user.TeamMember != null) &&
                    (user.TeamMember.AdditionalInfo != null);
        }

        public IEnumerable<string> GetCountries()
        {
            return SupportedCountries.GetCountries();
        }

        public TeamMember RegisterTeam(string teamId, string adminUsername, string adminPassword)
        {
            //TODO Assign to the admin user the TEAM NAME
            User user = UserFactory.CreateAdminUser(teamId, adminUsername, adminPassword);
            lock (_locker)
            {
                if (_dataBaseAPI.IsRegisteredTeamTask(teamId).Result)
                {
                    throw new TeamAlreadyRegisteredException();
                }
                if (_dataBaseAPI.IsUsernameExistTask(adminUsername).Result)
                {
                    throw new UsernameAlreadyExistsException();
                }
                _dataBaseAPI.InsertUserTask(user).Wait();
                _dataBaseAPI.MarkTeamAsRegisterTask(teamId).Wait();
            }
            return user.TeamMember;
        }
            
    }
}
