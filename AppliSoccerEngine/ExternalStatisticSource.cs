using AppliSoccerStatisticAPI.ExternalAPISource;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerEngine
{
    public class ExternalStatisticSource
    {
        private static IStatisticAPI _statisticAPI;
        public static IStatisticAPI GetStatisticAPI()
        {
            if(_statisticAPI == null)
            {
                _statisticAPI = new APIServer();
            }
            return _statisticAPI;
        }
    }
}
