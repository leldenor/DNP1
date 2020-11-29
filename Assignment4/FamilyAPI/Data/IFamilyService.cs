using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyAPI.Models;

namespace FamilyAPI.Data
{
    public interface IFamilyService
    {
        Task<IList<Family>> GetFamilies();
        Task RemoveFamily(int HouseNumber, string StreetName);
        Task AddFamily(Family family);
        Task UpdateAsync(Family family);
    }
}