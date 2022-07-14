using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels
{
    public class LeaguesInfo
    {
        public List<LeagueDetails> LeaguesDetails { get; set; }
        public string MainLeagueId { get; set; }
        public List<string> SecondLeaguesIds { get; set; }
    }

    public class LeagueDetails
    {
        public string  ExtId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public string LogoURL { get; set; }
    }

}
