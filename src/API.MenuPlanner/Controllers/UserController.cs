using API.MenuPlanner.Dtos;
using API.MenuPlanner.Entities;
using API.MenuPlanner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

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

        [HttpPost("login")]
        public async Task<ActionResult<LoginDto.Response>> Login([FromBody] LoginDto.Request loginRequest)
        {
            LoginDto.Response response = await _userService.Login(loginRequest);
            return Ok(response);
        }

        [HttpGet("profile")]
        public async Task<ActionResult<UserDto>> Profile()
        {
            var authorizationHeader = HttpContext.Request.Headers[HeaderNames.Authorization];
            UserDto user = await _userService.Profile(authorizationHeader);
            return Ok(user);
        }
    }
}
