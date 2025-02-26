using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SimpleCrudApp.BLL.Abstract;
using SimpleCrudApp.Models.Entities;


namespace SimpleCrudApp.API.Controllers.V1
{
    [Route("api/v1/user")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<User> countries = await _userService.GetAllAsync();
                return new JsonResult(countries);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while fetching countries.");
            }
        }

        [HttpGet("{userId}")]
        [Authorize]

        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                User user = await _userService.GetAsync(userId);

                if (userId == null)
                {
                    return NotFound(new { error = $"User with ID {userId} not found." });
                }

                return new JsonResult(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

        [HttpDelete("{userId}")]
        [Authorize]

        public async Task<IActionResult> Delete(string userId)
        {
            try
            {
                await _userService.DeleteAsync(userId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

    }
}
