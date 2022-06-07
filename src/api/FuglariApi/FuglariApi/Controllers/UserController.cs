using FuglariApi.RequestModels;
using FuglariApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuglariApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService service)
        {
            userService = service; 
        }
        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            return Ok(await userService.GetUserByEmail(email));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await userService.GetAllUsers());
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            return Ok(await userService.Register(registerRequest));
        }

        [HttpGet("get_or_create/{email}")]
        public async Task<IActionResult> GetOrCreateUser(string email)
        {
            return Ok(await userService.GetOrCreateUser(email));

        }

    }
}
