using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.ResponseObjects
{
    public class RestObjectWrraperSerializer
    {
        public static RestObjectWrraper Serialize(Object objToSer)
        {
            return new RestObjectWrraper() { Data = objToSer, Type = objToSer.GetType() };
        }
    }
}
