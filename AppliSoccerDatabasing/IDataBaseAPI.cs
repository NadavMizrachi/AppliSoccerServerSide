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
        public Task InsertTeamTask(Team team);
        public Task<bool> IsTeamExistTask(Team team);
        public Task<bool> IsRegisteredTeamTask(string teamID);
        public Task<bool> IsUsernameExistTask(string username);
        public Task InsertUserTask(User user);
        public Task MarkTeamAsRegisterTask(string teamId);
        public Task<List<TeamMember>> GetTeamMembers(string teamId);


    }
}
