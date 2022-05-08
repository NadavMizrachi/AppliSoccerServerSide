using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class Country
    {
        public String IDCode { get; set; }
        public String Name { get; set; }
        public List<League> Leagues { get; set; }
    }
}
