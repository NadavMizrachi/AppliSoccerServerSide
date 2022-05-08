using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.DBModels
{
    public class TestDBModel
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
