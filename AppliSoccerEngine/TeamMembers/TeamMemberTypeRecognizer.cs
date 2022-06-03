using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerEngine.TeamMembers
{
    public class TeamMemberTypeRecognizer
    {
        public static bool IsCoach(TeamMember teamMember)
        {
            if(teamMember == null || teamMember.AdditionalInfo == null)
            {
                return false;
            }
            if(teamMember.MemberType != MemberType.Staff)
            {
                return false;
            }
            StaffAdditionalInfo info = teamMember.AdditionalInfo as StaffAdditionalInfo;
            return info.IsCoach;
        }
    }
}
