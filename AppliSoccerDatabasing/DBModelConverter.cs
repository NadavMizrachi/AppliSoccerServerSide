using AppliSoccerDatabasing.DBModels;
using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerDatabasing
{
    class DBModelConverter
    {
        public static Team ConvertTeam(TeamDBModel teamDBModel)
        {
            Team team = new Team(teamDBModel.CountryName, teamDBModel.Name);
            team.LeagueName = teamDBModel.LeagueName;
            team.IsRegistered = teamDBModel.IsRegistred;
            return team;
        }

        public static TeamDBModel ConvertTeam(Team team)
        {
            return new TeamDBModel()
            {
                Id = team.Id,
                Name = team.Name,
                CountryName = team.CountryName,
                LeagueName = team.LeagueName,
                IsRegistred = team.IsRegistered
            };
        }
        public static List<Team> ConvertTeams(List<TeamDBModel> teamDBModels)
        {
            List<Team> teams = new List<Team>();
            foreach (var teamDBModel in teamDBModels)
            {
                teams.Add(ConvertTeam(teamDBModel));
            }
            return teams;
        }
    }
}
