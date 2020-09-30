using KFC_Clone.Models.DBModels;

namespace SaltingAndHashing.Models
{
    public interface ISecurityService
    {
        bool ConfirmPassword(string challengePassword, string existingPassword, byte[] existingSalt);
        User SecurePassword(User user, string password, int saltSize = 64);
    }
}