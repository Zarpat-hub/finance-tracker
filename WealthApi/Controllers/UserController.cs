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
        private readonly IUserFacade _userFacade;

        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [HttpGet]
        [Route("me")]
        [Authorize]
        public async Task<ActionResult<User>> GetMe()
        {
            User user = await _userFacade.GetCurrentUser();
            return Ok(user);
        }


        [HttpPost]
        [Route("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            await _userFacade.ChangePassword(changePasswordDTO);
            return Ok();
        }

        [HttpPost]
        [Route("edit")]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileDTO editProfileDTO) {

            await _userFacade.EditProfile(editProfileDTO);
            return Ok();
        }

        [HttpPost]
        [Route("img")]
        [Authorize]
        public async Task<ActionResult<string>> UploadProfileImg(IFormFile formFile)
        {
            string url = await _userFacade.ChangeUserImg(formFile);
            return Ok(url);
        }

    }
}
