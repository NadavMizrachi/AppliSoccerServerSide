using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class EventDetails
    {
        public String Description { get; set; }
        public Place Place { get; set; }
        public DateTime Date { get; set; }
        public List<Role> RolesRelevantion { get; set; }
    }
}
