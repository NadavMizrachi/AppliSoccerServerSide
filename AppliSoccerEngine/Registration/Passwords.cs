using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace AppliSoccerEngine.Registration
{
    public class Passwords
    {
        private static HashAlgorithm _hashAlgo = new SHA256CryptoServiceProvider();
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
