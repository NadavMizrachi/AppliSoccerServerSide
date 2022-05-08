using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.ResponseObjects
{
    /**
     * This Object holds objects that should be transfered through Rest API. 
     * Data property holds the actual object that should be transfered.
     * Type property holds the Type of the instance of the object that was assigned to
     * the Data property.
     * 
     * In order to serialize/deserialize this wrraper, 
     * use RestObjectWrraperSerializer/RestObjectWrraperDeserializer classes.
     */
    public class RestObjectWrraper
    {
        public Object Data { get; set; }
        public Type Type { get; set; }
    }
}
