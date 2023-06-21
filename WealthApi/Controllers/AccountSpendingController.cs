using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WealthApi.Core;
using WealthApi.Facades;

namespace WealthApi.Controllers
{
    [Route("/account")]
    [ApiController]
    public class AccountSpendingController : ControllerBase
    {
        private readonly IAccountSpendingFacade _accountSpendingFacade;

        public AccountSpendingController(IAccountSpendingFacade accountSpendingFacade)
        {
            _accountSpendingFacade = accountSpendingFacade;
        }

        [HttpPost]
        [Route("/spending")]
        [Authorize]
        public async Task<IActionResult> SetAccountSpending([FromBody]AccountSpending accountSpending)
        {
            await _accountSpendingFacade.SaveSpending(accountSpending);
            return Ok();
        }

        [HttpGet]
        [Route("/config")]
        [Authorize]
        public async Task<ActionResult<AccountConfig>> GetAccountSpending()
        {
            AccountConfig accountSpending = await _accountSpendingFacade.GetSpending();
            return Ok(accountSpending);
        }


    }
}
