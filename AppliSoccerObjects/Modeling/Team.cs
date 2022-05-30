using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.Modeling
{
    public class Team
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string LeagueName { get; set; }
        public bool IsRegistered { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public User Admin { get; set; }

        public Team(string countryName, string name)
        {
            Name = name;
            CountryName = countryName;
            Id = CountryName + "_" + Name;
        }

    }
}
