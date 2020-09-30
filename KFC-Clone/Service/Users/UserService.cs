using KFC_Clone.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFC_Clone.Models.Repository
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepo;

        public UserService(IRepository<User> repository)
        {
            _userRepo = repository;
        }

        public User GetUser(string email)
        {
           return _userRepo.TableNoTracking.Where(user => 
           user.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        public User AddUser(User user)
        {
            _userRepo.Insert(user);
            
            return user;
        }
    }
}