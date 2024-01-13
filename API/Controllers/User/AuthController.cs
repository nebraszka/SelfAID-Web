using Microsoft.AspNetCore.Mvc;
using SelfAID.CommonLib.Models.User;
using SelfAID.CommonLib.Dtos.User;
using SelfAID.CommonLib.Models;
using SelfAID.API.Services.UserService;

namespace SelfAID.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<User>>> Register(UserDto request)
        {
            var response = await _userService.Register(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserDto request)
        {
            var response = await _userService.Login(request);
            return Ok(response);
        }

        
    }
}