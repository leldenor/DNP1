using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyAPI.Models;

namespace FamilyAPI.Data
{
    public interface IFamilyService
    {
        Task<IList<Family>> GetFamilies();
        Family Family { get; set; }
        Task RemoveFamily(int HouseNumber, string StreetName);
        Task<Family> AddFamily(Family family);
        Task UpdateAsync(Family family);
    }
}