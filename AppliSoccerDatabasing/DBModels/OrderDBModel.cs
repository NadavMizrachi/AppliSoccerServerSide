using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.DBModels
{
    public class OrderDBModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        public String Title { get; set; }
        public DateTime SendingDate { get; set; }
        public String SenderId { get; set; }
        public String TeamId { get; set; }
        public List<DBEnums.Role> RolesReceivers { get; set; }
        public List<String> MemberIdsReceivers { get; set; }
        public String Content { get; set; }
        public List<String> MembersIdsAlreadyRead { get; set; }
        public String GameId { get; set; }
    }
}
