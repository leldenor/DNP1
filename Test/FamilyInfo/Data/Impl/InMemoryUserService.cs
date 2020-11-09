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
        private IList<User> Users;
        private readonly HttpClient client;

        public InMemoryUserService()
        {
            client = new HttpClient();
        }
        public async Task<User> ValidateUser(string userName, string password)
        {
            string message = await client.GetStringAsync("http://localhost:5004/User");
            IList<User> result = JsonSerializer.Deserialize<List<User>>(message);
            Users = result;
            User first = Users.FirstOrDefault(user => user.UserName.Equals(userName));
            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }

            return first;
        }
    }
}