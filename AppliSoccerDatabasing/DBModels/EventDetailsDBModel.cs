using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.DBModels
{
    public class EventDetailsDBModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CreatorId { get; set; }
        public string TeamId { get; set; }
        public List<string> ParticipantsIds { get; set; }
        public List<DBEnums.Role> ParticipantsRoles { get; set; }
        public PlaceDBModel Place { get; set; }
        public DBEnums.EventType Type { get; set; }
        public string Description { get; set; }
        public Object AdditionalInfo { get; set; }

        public void UpdateWithNewValues(EventDetailsDBModel eventWithNewValues)
        {
            this.AdditionalInfo = eventWithNewValues.AdditionalInfo;
            this.Description = eventWithNewValues.Description;
            this.EndTime = eventWithNewValues.EndTime;
            this.StartTime = eventWithNewValues.StartTime;
            this.ParticipantsIds = eventWithNewValues.ParticipantsIds;
            this.ParticipantsRoles = eventWithNewValues.ParticipantsRoles;
            this.Place = eventWithNewValues.Place;
            this.Type = eventWithNewValues.Type;
            this.Title = eventWithNewValues.Title;
        }
    }


    
}
