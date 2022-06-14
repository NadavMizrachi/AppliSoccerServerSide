using AppliSoccerDatabasing.DBModels;
using AppliSoccerObjects.Modeling;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public Task<User> GetTeamMemberOfUser(string username)
        {
           return Task.Run(() =>
           {
               var userDBModel = FindUser(username);
               User user = DBModelConverter.ConvertUser(userDBModel);
               return user;
           });
        }

        private UserDBModel FindUser(string username)
        {
            var userList = _collection.Find(user => user.UserName.Equals(username)).ToList();
            if (userList.Count == 0)
            {
                return null;
            }
            return userList.First();
        }

        public Task<TeamMember> UpdateMember(string userId, TeamMember memberNewDetails)
        {
            return Task.Run(() =>
            {
                var teamMemberDBModelNew = DBModelConverter.ConvertTeamMember(memberNewDetails);
                var userDBModel = FindUser(userId);
                userDBModel.TeamMember = teamMemberDBModelNew;
                var filter = Builders<UserDBModel>.Filter.Eq(user => user.TeamMember.ID, memberNewDetails.ID);
                var result = _collection.ReplaceOneAsync(filter, userDBModel).Result;
                return memberNewDetails;
            });
        }

        public Task<bool> RemoveUser(string userId)
        {
            return Task.Run(() =>
            {
                var res = _collection.DeleteOneAsync(user => user.UserName == userId).Result;
                Debug.WriteLine("res.DeletedCount : " + res.DeletedCount);
                bool wasRemoved = res.DeletedCount > 0;
                return wasRemoved;
            });
        }

        internal Task<bool> IsCoachExit(string teamId)
        {
            return _collection.Find(
                userDBModel => 
                    userDBModel.TeamMember.TeamId.Equals(teamId) &&
                    (userDBModel.TeamMember.AdditionalInfo as StaffAdditionalInfoDBModel).IsCoach
            ).AnyAsync();
        }

    }
}
