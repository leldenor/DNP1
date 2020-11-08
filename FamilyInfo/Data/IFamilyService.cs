using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyInfo.Models;

namespace FamilyInfo.Data
{
    public interface IFamilyService
    {
        Task AddAdultAsync(Adult adult);
        Task RemoveAdultAsync(int personId);
        Task<IList<Family>> GetFamiliesAsync();
        Family Family { get; set; }
        Task RemoveFamilyAsync(int HouseNumber, string StreetName);
        Task AddFamilyAsync(Family family);
        Task RemoveChildAsync(int childId);
        Task RemoveChildPetAsync(int petId);
        Task RemovePetAsync(int petId);
        Task AddChildAsync(Child child);
        Task AddPetAsync(Pet pet);
    }
}