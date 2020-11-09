using System.Threading.Tasks;
using FamilyInfo.Models;

namespace FamilyInfo.Data
{
    public interface IUserService
    {
        Task<User> ValidateUser(string userName, string password);
    }
}