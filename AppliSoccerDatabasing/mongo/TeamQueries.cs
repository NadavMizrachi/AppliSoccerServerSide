using AppliSoccerDatabasing.DBModels;
using AppliSoccerObjects.Modeling;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerDatabasing.mongo
{
    public class TeamQueries
    {
        IMongoCollection<TeamDBModel> _collection;

        public TeamQueries(IMongoCollection<TeamDBModel> teamsCollection)
        {
            _collection = teamsCollection;
        }

        public Task<List<Team>> GetUnregistredTeamsTask()
        {
            return Task.Run(() =>
            {
                List<TeamDBModel> objectsFromDB = _collection.Find(team => team.IsRegistred == false).ToList();
                List<Team> result = DBModelConverter.ConvertTeams(objectsFromDB);
                return result;
            });
        }

        public Task<List<Team>> GetUnregistredTeamsTask(string country)
        {
            return Task.Run(() =>
            {
                List<TeamDBModel> objectsFromDB =
                    _collection.Find(team => team.IsRegistred == false && team.CountryName.ToLower() == country.ToLower()).ToList();
                List<Team> result = DBModelConverter.ConvertTeams(objectsFromDB);
                return result;
            });
        }

        public Task<List<Team>> GetRegistredTeamsTask()
        {
            return Task.Run(() =>
            {
                List<TeamDBModel> objectsFromDB =
                    _collection.Find(team => team.IsRegistred == true).ToList();
                List<Team> result = DBModelConverter.ConvertTeams(objectsFromDB);
                return result;
            });
        }

        public Task InsertTeamTask(Team team)
        {
            TeamDBModel teamDBModel = DBModelConverter.ConvertTeam(team);
            return _collection.InsertOneAsync(teamDBModel);
        }

        public Task<bool> IsRegisteredTeamTask(string teamID)
        {
            return _collection.Find(teamFromDB => teamFromDB.Id == teamID && teamFromDB.IsRegistred).AnyAsync();
        }

        public Task<bool> IsTeamExistTask(Team team)
        {
            TeamDBModel teamDBModel = DBModelConverter.ConvertTeam(team);
            return _collection.Find(teamFromDB => teamFromDB.Id == team.Id).AnyAsync();
        }

        public Task MarkTeamAsRegisterTask(string teamId)
        {
            var filter = Builders<TeamDBModel>.Filter.Eq(team => team.Id, teamId);
            var updateDef = Builders<TeamDBModel>.Update.Set(team => team.IsRegistred, true);
            var options = new UpdateOptions { IsUpsert = true };
            return _collection.UpdateOneAsync(filter, updateDef, options);
        }

        public Task UpdateTeam(Team team)
        {
            string teamId = team.Id;
            var filter = Builders<TeamDBModel>.Filter.Eq(team => team.Id, teamId);
            var updateLeaguesIds =
                Builders<TeamDBModel>.Update
                .Set(team => team.ExtMainLeagueId, team.ExtMainLeagueId)
                .Set(team => team.ExtSeconderyCompetitionsIds, team.ExtSeconderyCompetitionsIds);
            var options = new UpdateOptions { IsUpsert = true };
            return _collection.UpdateOneAsync(filter, updateLeaguesIds, options);
        }

        public string GetMainLeagueIdOfTeam(string teamId)
        {
            var team = _collection.Find(team => team.Id == teamId).ToList().First();
            return team.ExtMainLeagueId;
        }

        public Team GetTeam(string teamId)
        {
            var team = _collection.Find(team => team.Id == teamId).ToList().First();
            return DBModelConverter.ConvertTeam(team);
        }

        public Team GetTeamByExtId(string teamExtId)
        {
            var team = _collection.Find(team => team.ExtTeamId == teamExtId).ToList().First();
            return DBModelConverter.ConvertTeam(team);
        }
    }
}
