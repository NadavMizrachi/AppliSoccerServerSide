using AppliSoccerStatisticAPI.ExternalAPISource.API_Football;
using AppliSoccerStatisticAPI.RestEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football.EndPointImp
{
    public class TeamsEndPoint : EndPoint
    {
        // End point configuration:
        public static readonly String EndPointName = "teams";
        
        // Input parameters
        public static readonly String IdParamName = "id";
        public static readonly String LeagueParamName = "league";
        public static readonly String SeasonParamName = "season";
        public static readonly String CountryParamName = "country";

        public TeamsEndPoint() : base()
        {
            _name = TeamsEndPoint.EndPointName;
        }
        
    }
}
