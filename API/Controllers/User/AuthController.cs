using Microsoft.AspNetCore.Mvc;
using SelfAID.CommonLib.Models.User;
using SelfAID.CommonLib.Dtos.User;
using SelfAID.CommonLib.Models;
using SelfAID.API.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace SelfAID.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;

        [HttpGet, Authorize]
        public ActionResult<ServiceResponse<string>> GetMe()
        {
            var userName = User?.Identity?.Name;

            if (userName == null)
            {
                return NotFound(new ServiceResponse<string> { Message = "Użytkownik nie jest zalogowany." });
            }

            return Ok(new ServiceResponse<string> { Data = userName });
        }

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

        [HttpGet("google-login")]
        public IActionResult LoginWithGoogle()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("signing-google")]
        public async Task<IActionResult> GoogleResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                return BadRequest("Autoryzacja nie powiodła się");

            var claims = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            // Get this user from your database
            var user = await ((UserService)_userService)._context.Users.FirstOrDefaultAsync(u => u.Username == name);
            
            var token = ((UserService)_userService).CreateToken(user);

            return Ok(new ServiceResponse<string> { Data = token });
            
        }
    }
}