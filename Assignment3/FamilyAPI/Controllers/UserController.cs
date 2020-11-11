using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyAPI.Data;
using FamilyAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController:ControllerBase
    {
        private IUserService UserService;

        public UserController(IUserService userService)
        {
            UserService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<User>> GetUser([FromQuery] string userName, [FromQuery] string password)
        {
            try
            {
                User user = await UserService.ValidateUser(userName, password);
                return Ok(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}