using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerEngine.TeamMembers
{
    public class TeamMemberFactory
    {
        public static TeamMember CreateAdminTeamMember(string teamId, string userName)
        {
            return new TeamMember
            {
                ID = userName,
                TeamId = teamId,
                Description = "Admin user",
            };
        }
    }
}
