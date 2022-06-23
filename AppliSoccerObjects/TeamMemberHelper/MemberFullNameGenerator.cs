using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.TeamMemberHelper
{
    public class MemberFullNameGenerator
    {
        public static string Generate(TeamMember teamMember)
        {
            if(teamMember == null)
            {
                return "";
            }
            return teamMember.FirstName + " " + teamMember.LastName;
        }
    }
}
