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
        private readonly IAccountTransactionFacade _accountSpendingFacade;

        public AccountConfigController(IAccountConfigFacade accountConfigFacade, IAccountTransactionFacade accountSpendingFacade)
        {
            _accountConfigFacade = accountConfigFacade;
            _accountSpendingFacade = accountSpendingFacade;
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
        public async Task<IActionResult> AddGoal([FromBody] NewGoalDTO newGoalDTO)
        {
            Goal goal = await _accountConfigFacade.AddNewGoal(newGoalDTO);
            return Ok(goal);
        }

        [HttpPost]
        [Route("/spending")]
        [Authorize]
        public async Task<IActionResult> AddAccountSpending([FromBody] AccountSpendingDTO accountSpending)
        {
            await _accountSpendingFacade.SaveSpending(accountSpending);
            return Ok();
        }

        [HttpPost]
        [Route("/income")]
        [Authorize]
        public async Task<IActionResult> AddSingleIncome([FromBody] SingleIncomeDTO singleIncomeDTO)
        {
            await _accountSpendingFacade.SaveIncome(singleIncomeDTO);

            return Ok();
        }

        [HttpGet]
        [Route("/balance")]
        [Authorize]
        public async Task<ActionResult<TransactionsBalanceDTO>> GetTransactionsBalance()
        {
            return Ok(await _accountSpendingFacade.GetTransactionsBalance());
        }
    }
}
