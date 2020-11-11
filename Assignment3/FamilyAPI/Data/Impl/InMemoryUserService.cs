using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyAPI.Models;

namespace FamilyAPI.Data.Impl
{
    public class InMemoryUserService : IUserService
    {
        private List<User> Users;

        public InMemoryUserService()
        {
            Users = new[]
            {
                new User
                {
                    Password = "123456",
                    SecurityLevel = 5,
                    UserName = "Admin",
                    Role = "Admin"
                },
                new User
                {
                Password = "654321",
                SecurityLevel = 4,
                UserName = "Adult",
                Role = "Adult"
                },
                new User
                {
                    Password = "123654",
                    SecurityLevel = 1,
                    UserName = "Child",
                    Role = "Child"
                }
            }.ToList();
        }

        public async Task<User> ValidateUser(string userName, string password)
        {
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