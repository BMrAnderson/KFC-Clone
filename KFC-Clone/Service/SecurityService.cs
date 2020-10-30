using KFC_Clone.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SaltingAndHashing.Models
{
    /// <summary>
    /// Ensure you have a database table with a username, password and salt field
    /// 
    /// STEP 1: 
    ///    
    /// Use this class in the CreateAccount POST Action Method
    /// after a new account / user is instantiated or created
    ///
    /// var salt = SecurityDataLayer.CreateSalt(128);
    /// user.Password = Convert.ToBase64String(SecurityDataLayer.Hash(user.Password, salt));
    ///
    /// Database.InsertUser(user);
    /// 
    /// STEP 2: 
    ///
    /// In the LogIn method first retrieve the user object from the database by the username
    /// 
    /// user.Password = Convert.ToBase64String(SecurityDataLayer.Hash(password, user.Salt));
    /// 
    /// //check if the passwords match
    /// var loginSuccessful = user.Password == password;
    /// </summary>
    public class SecurityService : ISecurityService
    {
        private string CreateSalt(int size)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] buffer = new byte[size];
                rng.GetBytes(buffer);
                return Convert.ToBase64String(buffer);
            }
        }

        private byte[] Hash(string value, string salt)
        {
            byte[] _value = Encoding.UTF8.GetBytes(value);
            byte[] _salt = Encoding.UTF8.GetBytes(salt);
            byte[] _saltedValue = _value.Concat(_salt).ToArray();

            return new SHA256Managed().ComputeHash(_saltedValue);
        }

        public bool ConfirmPassword(string challengePassword, string existingPassword,byte[] existingSalt)
        {
            string salt = Convert.ToBase64String(existingSalt);
            byte[] passwordHash = Hash(challengePassword, salt);
           
            challengePassword = Convert.ToBase64String(passwordHash);

            return challengePassword == existingPassword;
        }

        public User SecurePassword(User user, string password, int saltSize = 64)
        {
            string salt = CreateSalt(saltSize);
            byte[] hash = Hash(password, salt);

            user.Password = Convert.ToBase64String(hash);
            user.Salt = Convert.FromBase64String(salt);

            return user;
        }
    }
}