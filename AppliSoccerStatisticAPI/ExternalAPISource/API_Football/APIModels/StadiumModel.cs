using Newtonsoft.Json;
using System;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels
{
    public class StadiumModel
    {
        [JsonProperty("id")]
        public String ID { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("address")]
        public String Address { get; set; }

        [JsonProperty("city")]
        public String City { get; set; }

        [JsonProperty("capacity")]
        public int? Capacity { get; set; }
    }
}