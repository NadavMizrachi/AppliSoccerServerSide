using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.ResponseObjects
{
    public class RestObjectWrraperDeserializer<T>
    {
        public static T Deserialize(RestObjectWrraper wrraper)
        {
            return (T)Convert.ChangeType(wrraper.Data, wrraper.Type);
        }
    }
}
