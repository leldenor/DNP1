using System;
using System.Threading.Tasks;
using FamilyAPI.DataAccess;
using FamilyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyAPI.Data.Impl
{
    public class UserService:IUserService
    {
        public async Task<User> ValidateUser(string userName, string password)
        {
            using (FamilyContext ctx = new FamilyContext())
            {
                User first = await ctx.Users.FirstOrDefaultAsync(user => user.UserName.Equals(userName));
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
}