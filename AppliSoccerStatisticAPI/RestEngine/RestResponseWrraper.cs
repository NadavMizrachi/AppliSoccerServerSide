using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AppliSoccerStatisticAPI.RestEngine
{
    public class RestResponseWrraper
    {
        [JsonProperty("response")]
        public JArray Response{ get; set; }
    }
}
