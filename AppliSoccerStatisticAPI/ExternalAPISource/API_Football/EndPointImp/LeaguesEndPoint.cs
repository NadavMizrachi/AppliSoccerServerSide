using System;
using System.Collections.Generic;
using System.Text;
using AppliSoccerStatisticAPI.RestEngine;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football.EndPointImp
{
    public class LeaguesEndPoint : EndPoint
    {
        // End point configuration:
        public static readonly String endPointName = "leagues";

        // Input parameters
        public static readonly String IdParamName = "id";
        public static readonly String teamIdParamName = "team";
        public static readonly String seasonParamName = "season";
        public static readonly String currentParamName = "current";

        public LeaguesEndPoint() : base()
        {
            _name = LeaguesEndPoint.endPointName;
        }
    }
}
