using MongoDB.Bson.Serialization.Attributes;
using System;
using static AppliSoccerDatabasing.DBModels.DBEnums;

namespace AppliSoccerDatabasing.DBModels
{
    public class TeamMemberDBModel
    {
        [BsonId]
        public string ID { get; set; }
        public MemberType MemberType { get; set; }
        public string TeamId { get; set; }
        public string TeamName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate{ get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public Object AdditionalInfo { get; set; }

    }

}