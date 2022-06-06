using API.MenuPlanner.Entities;
using API.MenuPlanner.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.MenuPlanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody] User user)
        {
            await _userService.RegisterUserAsync(user);
            return Ok();
        }

    }
}
