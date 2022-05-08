
using System.Collections.Generic;

namespace AppliSoccerObjects.Modeling
{
    public class Staff : TeamMember
    {
        public bool IsCoach { get; set; }
        public List<Role> ManagedRoles { get; set; }
    }
}
