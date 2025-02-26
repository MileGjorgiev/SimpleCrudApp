using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCrudApp.BLL.Abstract;
using SimpleCrudApp.Models.DTO;

namespace SimpleCrudApp.API.Controllers.V1
{
    [Route("api/v1/auth")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            var result = await _userService.Register(registerDto);
            if (!result)
            {
                return BadRequest("Registration failed.");
            }
            return Ok("Registration successful.");
        }

       
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var authResponse = await _userService.Login(loginDto);
            if (authResponse == null)
            {
                return Unauthorized("Invalid credentials.");
            }
            return Ok(authResponse);
        }
    }
}
