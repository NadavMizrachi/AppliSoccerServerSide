using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing.DBModels
{

    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(PlayerAdditionalInfoDBModel), typeof(StaffAdditionalInfoDBModel))]
    public class AdditionalInfoDBModel
    {
    }
}
