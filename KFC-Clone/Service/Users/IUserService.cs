using KFC_Clone.Models.DBModels;

namespace KFC_Clone.Models.Repository
{
    public interface IUserService
    {
        User AddUser(User user);
        User GetUser(string email);
    }
}