using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels
{
    public class TeamInformationsModel
    {
        [JsonProperty("team")]
        public TeamModel Team { get; set; }

        [JsonProperty("venue")]
        public StadiumModel Stadium { get; set; }

    }
}
