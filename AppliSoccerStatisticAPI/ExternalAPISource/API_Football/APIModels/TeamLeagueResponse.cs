using AppliSoccerObjects.Modeling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels
{
    public class TeamLeagueResponse
    {
        [JsonProperty("league")]
        public LeagueModel League { get; set; }

        [JsonProperty("country")]
        
        public CountryModel Country { get; set; }
        [JsonProperty("seasons")]
        public List<SeasonModel> Seasons { get; set; }
            
    }

    public class LeagueModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("logo")]
        public string LogoURL { get; set; }
    }

    public class CountryModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class SeasonModel
    {
        [JsonProperty("year")]
        public string Year { get; set; }
        [JsonProperty("current")]
        public bool Current { get; set; }

    }
}
