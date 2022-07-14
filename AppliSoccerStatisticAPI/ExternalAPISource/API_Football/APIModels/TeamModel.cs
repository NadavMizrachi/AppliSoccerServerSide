using Newtonsoft.Json;
using System;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels
{
    public class TeamModel
    {
        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("code")]
        public String Code { get; set; }

        [JsonProperty("country")]
        public String CountryName { get; set; }

        [JsonProperty("founded")]
        public int? FoundedYear { get; set; }
        
        [JsonProperty("logo")]
        public string LogoUrl { get; set; }
    }
}