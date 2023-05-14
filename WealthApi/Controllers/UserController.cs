using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WealthApi.Contracts;
using WealthApi.Database.Models;
using WealthApi.Services;

namespace WealthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("me")]
        [Authorize]
        public async Task<ActionResult<User>> GetMe()
        {
            User user = await _userService.GetCurrentUser();
            return Ok(user);
        }
    }
}
