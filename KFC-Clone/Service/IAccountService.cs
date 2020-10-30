using KFC_Clone.Models.DBModels;

namespace KFC_Clone.Lib
{
    public interface IAccountService
    {
        User CreateAccount(string email, string name, string phoneNumber, string password);
        User LogIn(string email, string password);
    }
}