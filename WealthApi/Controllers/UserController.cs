using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WealthApi.Contracts;
using WealthApi.Database.Models;
using WealthApi.Facades;

namespace WealthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserFacade _userService;

        public UserController(IUserFacade userService)
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


        [HttpPost]
        [Route("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            await _userService.ChangePassword(changePasswordDTO);
            return Ok();
        }

    }
}
