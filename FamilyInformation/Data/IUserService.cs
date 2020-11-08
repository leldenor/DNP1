using FamilyInformation.Models;

namespace FamilyInformation.Data
{
    public interface IUserService
    {
        User ValidateUser(string userName, string password);
    }
}