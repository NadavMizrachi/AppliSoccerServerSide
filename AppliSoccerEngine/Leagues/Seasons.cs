using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerEngine.Leagues
{
    public class Seasons
    {
        public static string GetCurrentSeason()
        {
            string currentSeasonFromConfig = MyConfiguration.GetStringFromAppSetting("currentSeason");
            if(currentSeasonFromConfig.Length > 0)
            {
                return currentSeasonFromConfig;
            }
            else
            {
                return "2021";
            }
        }
    }
}
