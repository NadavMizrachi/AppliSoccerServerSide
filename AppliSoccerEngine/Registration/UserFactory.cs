using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerEngine.Registration
{
    public class UserFactory
    {
        public static User CreateAdminUser(string teamId, string adminUsername, string adminPassword)
        {
            return new User()
            {
                Username = adminUsername,
                Password = Passwords.HashPassword(adminPassword),
                IsAdmin = true
            };
        }
    }
}
