using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.ActionResults.LeagueActions
{
    public enum GetLeagueFailReason { 
        Unknown,
        LeagueNotExist,
        LeagueNotAvailable 
    }
    public class GetMainLeagueActionResult : ActionResult
    {
        public GetLeagueFailReason FailReason { get; set; }
        public League MainLeague { get; set; }
    }
}
