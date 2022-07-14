using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.DBModels
{
    public class LeagueDBModel
    {
        [BsonId]
        public string Id { get; set; }
        public string LogoUrl { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public LeagueTableDBModel Table { get; set; }

    }
}
