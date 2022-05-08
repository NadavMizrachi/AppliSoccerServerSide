using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class User
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public TeamMember TeamMember { get; set; }
        public Boolean IsAdmin { get; set; }
    }
}
