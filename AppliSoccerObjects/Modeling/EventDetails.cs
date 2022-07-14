using System;
using System.Collections.Generic;

namespace AppliSoccerObjects.Modeling
{
    public class EventDetails
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CreatorId { get; set; }
        public string TeamId { get; set; }
        public List<string> ParticipantsIds { get; set; }
        public List<Role> ParticipantsRoles { get; set; }
        public Place Place { get; set; }
        public EventType Type { get; set; }
        public string Description { get; set; }
        public Object AdditionalInfo{ get; set; }
    }
}
