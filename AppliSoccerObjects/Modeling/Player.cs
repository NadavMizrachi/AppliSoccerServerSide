using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class Player : TeamMember
    {
        public int Number { get; set; }
        public Role Role { get; set; }
    }
}
