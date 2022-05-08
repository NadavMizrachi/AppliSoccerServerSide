using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerStatisticAPI.RestEngine
{
    public class ResponseDeserializer
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // The response has 'response' field inside it.
        // In this field, there is an array of objects, therefore we need 
        // return a list of objects.
        public static List<T> DeserializeAsObjectList<T>(RestResponse response)
        {
            if (!response.IsSuccessful)
            {
                _logger.Error("Response is not successfull.");
                throw new Exception("Unsuccessfull request!");
            }
            RestResponseWrraper wrraper = 
                JsonConvert.DeserializeObject<RestResponseWrraper>(response.Content);
            List<T> deserialized = JsonConvert.DeserializeObject<List<T>>(wrraper.Response.ToString());
            return deserialized;
        }
    }
}
