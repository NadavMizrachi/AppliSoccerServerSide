using AppliSoccerEngine.Leagues;
using AppliSoccerEngine.Registration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerEngine
{
    public class StartUpManager
    {
        public static void MakeStartUpInits()
        {
            if (MyConfiguration.GetBoolFromAppSetting("pullTeams"))
            {
                RegistrationManager.PullTeamsToDatabase();
            }
            if (MyConfiguration.GetBoolFromAppSetting("pullTeamLeagues"))
            {
                LeaguesManager.PullTeamLeaguesIds();
            }
            LeaguesManager.StartLoadLeagueTable();
        }
    }
}
