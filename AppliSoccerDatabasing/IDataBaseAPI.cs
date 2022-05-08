using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerDatabasing
{
    public interface IDataBaseAPI
    {
        public void CreateDatabase();
        public void CreateTables();
        public Task<List<Team>> GetUnregistredTeamsTask();
        public Task<List<Team>> GetUnregistredTeamsTask(string country);
        public void InsertTeam(Team team);
        public Task<bool> IsTeamExistTask(Team team);
        public Task<bool> IsRegisteredTeamTask(string teamID);
        public Task<bool> IsUsernameExistTask(string username);
        public Task InsertUser(User user);
        public Task MarkTeamAsRegister(string teamId);


    }
}
