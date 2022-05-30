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
    public class UserQueries
    {
        IMongoCollection<UserDBModel> _collection;

        public UserQueries(IMongoCollection<UserDBModel> usersCollection)
        {
            _collection = usersCollection;
        }

        public Task<bool> IsUsernameExistTask(string username)
        {
            return _collection.Find(userFromDB => userFromDB.UserName.Equals(username)).AnyAsync();
        }

        public Task InsertUserTask(User user)
        {
            UserDBModel userDBModel = DBModelConverter.ConvertUser(user);
            return _collection.InsertOneAsync(userDBModel);
        }

        public Task<List<TeamMember>> GetTeamMembers(string teamId)
        {
            return Task.Run(() =>
            {
                List<UserDBModel> userObjectsFromDB =
                    _collection.Find(user => user.TeamMember != null && user.TeamMember.TeamId == teamId).ToList();
                List<TeamMemberDBModel> teamMemberDBModels = userObjectsFromDB.Select(user => user.TeamMember).ToList();
                List<TeamMember> result = DBModelConverter.ConvertTeamMembers(teamMemberDBModels);
                return result;
            });
        }
    }
}
