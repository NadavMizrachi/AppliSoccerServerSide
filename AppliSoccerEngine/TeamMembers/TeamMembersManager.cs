using AppliSoccerDatabasing;
using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerEngine.TeamMembers
{
    public class TeamMembersManager
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDataBaseAPI _dataBaseAPI = Database.GetDatabase();

        public Task<List<TeamMember>> GetTeamMembers(string teamId)
        {
            return _dataBaseAPI.GetTeamMembers(teamId);
        }

        public Task<TeamMember> UpdateMemberDetails(TeamMember memberNewDetails)
        {
            // TODO create class that returns user id we providing that function TeamMember/User
            return _dataBaseAPI.UpdateMember(userId: memberNewDetails.ID, memberNewDetails);
        }

        public Task<bool> RemoveMember(TeamMember memberToRemove)
        {
            return _dataBaseAPI.RemoveUser(memberToRemove.ID);
        }
    }
}
