using Microsoft.AspNetCore.Mvc;
using WealthApi.Contracts;
using WealthApi.Facades;

namespace WealthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthenticationFacade _authenticationFacade;

        public AuthController(ILogger<AuthController> logger, IAuthenticationFacade authenticationFacade)
        {
            _logger = logger;
            _authenticationFacade = authenticationFacade;
        }

        [HttpPost]
        [Route("attempt")]
        public async Task<IActionResult> AttemptRegistration([FromBody] UserRegisterDTO userDTO)
        {
            await _authenticationFacade.AttemptRegistration(userDTO);

            return Ok();
        }

        [HttpPost]
        [Route("confirm/{token}")]
        public async Task<IActionResult> ConfirmRegistration([FromRoute] string token)
        {
            await _authenticationFacade.ConfirmRegistration(token);

            return Ok();
        }

        [HttpPost]
        [Route("/token")]
        public async Task<ActionResult<string>> AttemptLogin([FromBody] UserLoginDTO userLoginDTO)
        {
            try
            {
                string token = await Task.Run(() => _authenticationFacade.GetAuthorizationToken(userLoginDTO));

                return Ok(token);
            } catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}