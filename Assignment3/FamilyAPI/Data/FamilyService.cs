using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyAPI.Models;

namespace FamilyAPI.Data
{
    public class FamilyService
    {
        private FileContext file;
        public IList<Family> Families { get; private set; }
        

        public async Task<IList<Family>> GetFamilies()
        {
            IList<Family> families= await file.GetFamilies();
            Families = families;
            return families;
        }

        public Family Family { get; set; }
        public async Task RemoveFamily(int HouseNumber, string StreetName)
        {
           await file.RemoveFamily(HouseNumber, StreetName);
        }

        public async Task<Family> AddFamily(Family family)
        {
            await file.AddFamily(family);
            return family;
        }
        
        public async Task UpdateAsync(Family family)
        {
            Family = family;
        }
    }
}