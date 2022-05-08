using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;

namespace AppliSoccerEngine.Registration
{
    public class Passwords
    {
        public static string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        public static bool IsVerified(string hashedPassword, string inputPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }
    }
}
