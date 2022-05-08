using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.DBModels
{
    public class UserDBModel
    {
        [BsonId]
        public string UserName { get; set; }
        public string Password { get; set; }
        public TeamMemberDBModel TeamMember { get; set; }
        public Boolean IsAdmin { get; set; }
    }

    /*
     public class User
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public TeamMember TeamMember { get; set; }
        public Boolean IsAdmin { get; set; }
    }
     */
}
