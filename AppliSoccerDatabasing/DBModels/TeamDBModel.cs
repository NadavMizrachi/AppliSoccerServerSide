using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.DBModels
{
    public class TeamDBModel
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string LeagueName { get; set; }
        public bool IsRegistred { get; set; }

        /**
         * public string Id { get; private set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string LeagueName { get; set; }
        public bool IsRegistered { get; set; }
        public List<Player> Players { get; set; }
        public List<Staff> StaffMembers{ get; set; }
        public User Admin { get; set; }
         */
    }
}
