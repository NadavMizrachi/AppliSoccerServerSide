using AppliSoccerDatabasing.DBModels;
using AppliSoccerObjects.Modeling;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerDatabasing.mongo
{
    public class MongoDatabase : IDataBaseAPI
    {
        private CollectionsAccess _collectionAccess;
        public MongoDatabase()
        {
            _collectionAccess = new CollectionsAccess();
        }
        public void CreateDatabase()
        {
            // Database will be created when accessing it
        }

        public void CreateTables()
        {
            // Collections will be created when accessing it
        }

        public Task<List<Team>> GetUnregistredTeamsTask()
        {
            return Task.Run(() =>
           {
               IMongoCollection<TeamDBModel> teamsCollection = _collectionAccess.GetTeamsCollection();
               List<TeamDBModel> objectsFromDB = teamsCollection.Find(team => team.IsRegistred == false).ToList();
               List<Team> result = DBModelConverter.ConvertTeams(objectsFromDB);
               return result;
           });
        }

        public Task<List<Team>> GetUnregistredTeamsTask(string country)
        {
            return Task.Run(() =>
            {
                IMongoCollection<TeamDBModel> teamsCollection = _collectionAccess.GetTeamsCollection();
                List<TeamDBModel> objectsFromDB =
                    teamsCollection.Find(team => team.IsRegistred == false && team.CountryName.ToLower() == country.ToLower()).ToList();
                List<Team> result = DBModelConverter.ConvertTeams(objectsFromDB);
                return result;
            });
        }

        public void InsertTeam(Team team)
        {
            IMongoCollection<TeamDBModel> teamsCollection = _collectionAccess.GetTeamsCollection();
            TeamDBModel teamDBModel = DBModelConverter.ConvertTeam(team);
            teamsCollection.InsertOne(teamDBModel);
        }

        public Task InsertUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRegisteredTeam(string teamID)
        {
            IMongoCollection<TeamDBModel> teamsCollection = _collectionAccess.GetTeamsCollection();
            return teamsCollection.Find(teamFromDB => teamFromDB.Id == teamID && teamFromDB.IsRegistred).AnyAsync();
        }

        public Task<bool> IsRegisteredTeamTask(string teamID)
        {
            throw new NotImplementedException();
        }


        public Task<bool> IsTeamExistTask(Team team)
        {
            IMongoCollection<TeamDBModel> teamsCollection = _collectionAccess.GetTeamsCollection();
            TeamDBModel teamDBModel = DBModelConverter.ConvertTeam(team);
            return teamsCollection.Find(teamFromDB => teamFromDB.Id == team.Id).AnyAsync();
        }

        public Task<bool> IsUsernameExistTask(string username)
        {
            throw new NotImplementedException();
        }

        public Task MarkTeamAsRegister(string teamId)
        {
            throw new NotImplementedException();
        }
    }
}
