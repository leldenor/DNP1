using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyAPI.Models;

namespace FamilyAPI.Data
{
    public interface IUserService
    {
        Task<User> ValidateUser(string userName, string password);
    }
}