using AppliSoccerObjects.Modeling;
using AppliSoccerStatisticAPI.ExternalAPISource.API_Football.APIModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerStatisticAPI.ExternalAPISource.API_Football
{
    public class ModelToObjectConverter
    {
        public static Team ConvertTeam(TeamInformationsModel teamInformations)
        {
            Team team = new Team(teamInformations.Team.CountryName, teamInformations.Team.Name);
            return team;
        }

        public static List<Team> ConvertTeams(List<TeamInformationsModel> teamInformationsModels)
        {
            var teams = new List<Team>();
            teamInformationsModels.ForEach(teamInfo => teams.Add(ModelToObjectConverter.ConvertTeam(teamInfo)));
            return teams;
        }
    }
}
