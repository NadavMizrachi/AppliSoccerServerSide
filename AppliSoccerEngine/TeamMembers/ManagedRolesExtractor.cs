using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerEngine.TeamMembers
{
    public class ManagedRolesExtractor
    {
        public static List<Role> Extract(TeamMember member)
        {
            if(TeamMemberTypeRecognizer.IsStaff(member) && !TeamMemberTypeRecognizer.IsCoach(member))
            {
                return (member.AdditionalInfo as StaffAdditionalInfo).ManagedRoles;
            }
            else
            {
                return new List<Role>();
            }
        }
    }
}
