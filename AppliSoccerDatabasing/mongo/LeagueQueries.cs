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
    public class LeagueQueries
    {
        IMongoCollection<LeagueDBModel> _collection;

        public LeagueQueries(IMongoCollection<LeagueDBModel> leaguesCollection)
        {
            _collection = leaguesCollection;
        }

        /* Updates all details except table ranks */
        public async Task UpdateLeaguesDetails(List<League> leagues)
        {
            List<LeagueDBModel> leagueModels = DBModelConverter.ConvertLeagues(leagues);
            foreach (var leagueModel in leagueModels)
            {
                await UpdateLeaguesDetail(leagueModel); 
            }
        }

        /* Updates all details except table ranks */
        public Task UpdateLeaguesDetail(LeagueDBModel leagueModel)
        {
            var filter = Builders<LeagueDBModel>.Filter.Eq(lg => lg.Id, leagueModel.Id);
            var updateDetailsWithoutTableRanks =
                Builders<LeagueDBModel>.Update
                .Set(lg => lg.Country, leagueModel.Country)
                .Set(lg => lg.LogoUrl, leagueModel.LogoUrl)
                .Set(lg => lg.Name, leagueModel.Name);
            var options = new UpdateOptions { IsUpsert = true };
            return _collection.UpdateOneAsync(filter, updateDetailsWithoutTableRanks, options);
        }

        public Task UpdateTableRanks(string leagueId, LeagueTable leagueTable)
        {
            LeagueTableDBModel tableModel = DBModelConverter.ConvertLeagueTable(leagueTable);
            var filter = Builders<LeagueDBModel>.Filter.Eq(lgTb => lgTb.Id, leagueId);
            var updateTableRank = Builders<LeagueDBModel>.Update
                .Set(lg => lg.Table, tableModel);
            var options = new UpdateOptions { IsUpsert = true };
            return _collection.UpdateOneAsync(filter, updateTableRank, options);
        }

        public async Task<League> GetMainLeague(string extMainLeagueId)
        {
            // Bring the mainLeague id
            var leagueModelList = (await _collection.FindAsync(lm => lm.Id == extMainLeagueId)).ToList();
            if (leagueModelList == null || leagueModelList.Count == 0)
                return null;
            return DBModelConverter.ConvertLeague(leagueModelList.First());
        }
    }
}
