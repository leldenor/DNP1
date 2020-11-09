using System.Threading.Tasks;
using FamilyInfo.Models;

namespace FamilyInfo.Data
{
    public interface IUserService
    {
        User ValidateUser(string userName, string password);
    }
}