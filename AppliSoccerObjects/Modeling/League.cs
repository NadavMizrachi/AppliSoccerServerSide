using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class League
    {
        public string ID { get; set; }
        public string LogoUrl { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public LeagueTable Table { get; set; }
    }
}
