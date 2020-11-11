using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FamilyInfo.Models;

namespace FamilyInfo.Data
{
    public class FamilyServiceClient : IFamilyService
    {
        private readonly HttpClient client;
        public IList<Family> Families { get; private set; }

        public FamilyServiceClient()
        {
            client = new HttpClient();
        }

        public async Task AddAdultAsync(Adult adult)
        {
            IList<Adult> adults= Families.SelectMany(item => item.Adults).ToList();
            int max = adults.Max(a => a.Id);
            adult.Id = (++max);
            Family.Adults.Add(adult);
            await UpdateFamily(Family);
        }

        public async Task RemoveAdultAsync(int personId)
        {
            Adult toRemove = Family.Adults.First(a => a.Id == personId);
            Family.Adults.Remove(toRemove);
            await UpdateFamily(Family);
        }

        public async Task<IList<Family>> GetFamiliesAsync()
        {
            string message = await client.GetStringAsync("http://localhost:5004/Family");
            IList<Family> result = JsonSerializer.Deserialize<List<Family>>(message);
            Families = result;
            return result;
        }

        public Family Family { get; set; }
        public async Task RemoveFamilyAsync(int HouseNumber, string StreetName)
        {
            await client.DeleteAsync($"http://localhost:5004/Family?streetName={StreetName}&houseNumber{HouseNumber}");
        }

        public async Task AddFamilyAsync(Family family)
        {
            string familySerialized = JsonSerializer.Serialize(family);
            HttpContent content=new StringContent(
                familySerialized,
                Encoding.UTF8,
                "application/json"
                );
            var response = await client.PostAsync("http://localhost:5004/Family", content);
            Console.WriteLine(response.StatusCode);
        }

        public async Task RemoveChildAsync(int childId)
        {
            Child toRemove = Family.Children.First(a => a.Id == childId);
            Family.Children.Remove(toRemove);
            await UpdateFamily(Family);
        }

        public async Task RemoveChildPetAsync(int petId)
        {
        }

        public async Task RemovePetAsync(int petId)
        {
            Pet toRemove = Family.Pets.First(a => a.Id == petId);
            Family.Pets.Remove(toRemove);
            await UpdateFamily(Family);
        }

        public async Task AddChildAsync(Child child)
        {
            IList<Child> children= Families.SelectMany(item => item.Children).ToList();
            int max = children.Max(child => child.Id);
            child.Id = (++max);
            Family.Children.Add(child);
            await UpdateFamily(Family);
        }

        public async Task AddPetAsync(Pet pet)
        {
            IList<Pet> pets= Families.SelectMany(item => item.Pets).ToList();
            foreach (var item in Families.SelectMany(item => item.Children.SelectMany(t => t.Pets)).ToList())
            {
                pets.Add(item);
            }
        
            int max = pets.Max(petId => petId.Id);
            pet.Id = (++max);
            Family.Pets.Add(pet);
            await UpdateFamily(Family);
        }

        private async Task UpdateFamily(Family family)
        {
            string familySerialized = JsonSerializer.Serialize(family);
            HttpContent content=new StringContent(
                familySerialized,
                Encoding.UTF8,
                "application/json"
                );
            var response = await client.PatchAsync($"http://localhost:5004/Family/{family.StreetName}&{family.HouseNumber}", content);
            Console.WriteLine(response.StatusCode);
        }
    }
}