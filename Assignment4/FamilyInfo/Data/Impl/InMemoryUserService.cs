using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FamilyInfo.Models;

namespace FamilyInfo.Data.Impl
{
    public class InMemoryUserService : IUserService
    {
        private readonly HttpClient client;

        public InMemoryUserService()
        {
            client = new HttpClient();
        }
        public async Task<User> ValidateUser(string userName, string password)
        {
            string message = await client.GetStringAsync($"http://localhost:5004/User?username={userName}&password={password}");
            User result = JsonSerializer.Deserialize<User>(message);

            return result;
        }
    }
}