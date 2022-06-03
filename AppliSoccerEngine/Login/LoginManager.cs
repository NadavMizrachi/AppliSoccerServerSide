using AppliSoccerDatabasing;
using AppliSoccerEngine.Registration;
using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerEngine.Login
{
    public class LoginManager
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDataBaseAPI _dataBaseAPI = Database.GetDatabase();

        public async Task<TeamMember> Login(string username, string password)
        {
            User userWithSameUsername = await _dataBaseAPI.GetUser(username);
            if(userWithSameUsername != null && Passwords.IsVerified(userWithSameUsername.Password, password))
            {
                return userWithSameUsername.TeamMember;
            }
            return null;
        }
    }
}
