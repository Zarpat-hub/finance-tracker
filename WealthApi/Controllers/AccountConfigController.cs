using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WealthApi.Contracts;
using WealthApi.Core;
using WealthApi.Facades;

namespace WealthApi.Controllers
{
    [ApiController]
    [Route("/account")]
    public class AccountConfigController : ControllerBase
    {
        private readonly IAccountConfigFacade _accountConfigFacade;

        public AccountConfigController(IAccountConfigFacade accountConfigFacade)
        {
            _accountConfigFacade = accountConfigFacade;
        }

        [HttpPost]
        [Route("/config")]
        [Authorize]
        public async Task<IActionResult> SetAccountConfig([FromBody]AccountConfig accountConfig)
        {
            await _accountConfigFacade.SaveConfig(accountConfig);
            return Ok();
        }

        [HttpGet]
        [Route("/config")]
        [Authorize]
        public async Task<ActionResult<AccountConfig>> GetAccountConfig()
        {
            AccountConfig accountConfig = await _accountConfigFacade.GetConfig();
            return Ok(accountConfig);
        }

        [HttpPost]
        [Route("/goal")]
        [Authorize]
        public async Task<IActionResult> AddGoal(NewGoalDTO newGoalDTO)
        {
            Goal goal = await _accountConfigFacade.AddNewGoal(newGoalDTO);
            return Ok(goal);
        }
        
    }
}
