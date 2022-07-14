using AppliSoccerStatisticAPI.RestEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football.EndPointImp
{
    class StandingEndPoint : EndPoint
    {
        // End point configuration:
        public static readonly String endPointName = "standings";

        // Input parameters
        public static readonly String leagueIdParamName = "league";
        public static readonly String seasonParamName = "season";

        public StandingEndPoint() : base()
        {
            _name = endPointName;
        }
    }
}
