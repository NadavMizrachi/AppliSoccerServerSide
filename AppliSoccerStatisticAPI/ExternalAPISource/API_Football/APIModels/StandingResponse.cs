using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels
{
    /*
     "league":{7 items
        "id":39
        "name":"Premier League"
        "country":"England"
        "logo":"https://media.api-sports.io/football/leagues/39.png"
        "flag":"https://media.api-sports.io/flags/gb.svg"
        "season":2020
        "standings":[1 item
            0:[20 items
                0:{12 items
                    "rank":1
                    "team":{3 items
                        "id":50
                        "name":"Manchester City"
                        "logo":"https://media.api-sports.io/football/teams/50.png"
                        }
                    "points":74
                    "goalsDiff":45
                    "group":"Premier League"
                    "form":"WWWLW"
                    "status":"same"
                    "description":"Promotion - Champions League (Group Stage)"
                    "all":{...}5 items
                    "home":{...}5 items
                    "away":{...}5 items
                    "update":"2021-04-05T00:00:00+00:00"
}
     */
    public class StandingResponse
    {
        [JsonProperty("league")]
        public StandingLeague StandingLeague { get; set; }

    }

    public class StandingLeague
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("country")]
        public string Country { get; set; }
        
        [JsonProperty("logo")]
        public string LogoURL { get; set; }
        
        [JsonProperty("flag")]
        public string FlagURL { get; set; }

        [JsonProperty("standings")]
        public JArray Standings { get; set; }
    }
        
    public class Standing
    {
        public JArray StandingRows { get; set; }
    }

    public class StandingRow
    {
        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("team")]
        public StandingTeam Team { get; set; }
        
        [JsonProperty("points")]
        public int Points { get; set; }
       
        [JsonProperty("goalsDiff")]
        public int GoalsDiff { get; set; }

        [JsonProperty("form")]
        public string Form { get; set; }
        [JsonProperty("group")]
        public string Group { get; set; }
    }

    public class StandingTeam
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

    }

}
