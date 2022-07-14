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
        public string LogoUrl { get; set; }
        public string ExtMainLeagueId { get; set; }
        public string ExtTeamId { get; set; }
        public List<string> ExtSeconderyCompetitionsIds { get; set; }
    }
}
