using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KFC_Clone.Models.DBModels;
using KFC_Clone.Models.Repository;
using KFC_Clone.Service;
using SaltingAndHashing.Models;

namespace KFC_Clone.Lib
{
    public class AccountService : IAccountService
    {
        private IUserService _userRepository;
        private ISecurityService _securityService;

        //using dependency injection
        public AccountService(IUserService repository, ISecurityService security)
        {
            _userRepository = repository;
            _securityService = security;
        }

        public User CreateAccount(string email, string name, string phoneNumber, string password)
        {
            var user = _userRepository.GetUser(email);

            if (user != null)
            {
                throw new Exception("Account Exists");
            }

            user = new User()
            {
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber,
                Password = password
            };

            user = _securityService.SecurePassword(user, password);

            _userRepository.AddUser(user);

            return user;
        }

        public User LogIn(string email, string password)
        {
            var user = _userRepository.GetUser(email);

            if (user == null)
            {
                throw new Exception("Invalid Username/Password");
            }

            bool isSuccess = _securityService.ConfirmPassword(password, user.Password, user.Salt);
            
            if (isSuccess)
            {
                return user;
            }

            return null;
        }
    }
}