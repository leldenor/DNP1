using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using FamilyAPI.Models;

namespace FamilyAPI.Data.Impl
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime JsRuntime;
        private readonly IUserService UserService;

        private User cachedUser;

        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IUserService userService)
        {
            JsRuntime = jsRuntime;
            UserService = userService;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity=new ClaimsIdentity();
            if (cachedUser == null)
            {
                string userAsJson = await JsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    cachedUser = JsonSerializer.Deserialize<User>(userAsJson);
                    identity = SetUpClaimsForUser(cachedUser);
                }
            }
            else
            {
                identity = SetUpClaimsForUser(cachedUser);
            }
            ClaimsPrincipal cachedClaimsPrincipal=new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
        }

        public void ValidateLogin(string userName, string password)
        {
            Console.WriteLine("Validating log in");
            if(string.IsNullOrEmpty(userName))throw new Exception("Enter username");
            if(string.IsNullOrEmpty(password))throw new Exception("Enter password");
            
            ClaimsIdentity identity =new ClaimsIdentity();
            try
            {
                User user = UserService.ValidateUser(userName, password);
                identity = SetUpClaimsForUser(user);
                string serialisedData = JsonSerializer.Serialize(user);
                JsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
                cachedUser = user;
            }
            catch (Exception e)
            {
                throw e;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }

        public void Logout()
        {
            cachedUser = null;
            var user=new ClaimsPrincipal(new ClaimsIdentity());
            JsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
        private ClaimsIdentity SetUpClaimsForUser(User user)
        {
            List<Claim> claims=new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("Level",user.SecurityLevel.ToString()));
            claims.Add(new Claim("Role",user.Role));
            ClaimsIdentity identity=new ClaimsIdentity(claims, "auth_type");
            return identity;
        }
    }
}