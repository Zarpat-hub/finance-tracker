using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using WealthApi.Contracts;
using WealthApi.Core;
using WealthApi.Core.Enums;
using WealthApi.Database;
using WealthApi.Database.Models;

namespace WealthApi.Facades
{
    public interface IAccountTransactionFacade
    {
        Task SaveSpending(AccountSpendingDTO spending);
        Task SaveIncome(SingleIncomeDTO singleIncomeDTO);
        Task<TransactionsBalanceDTO> GetTransactionsBalance();
    }

    public class AccountTransactionFacade : IAccountTransactionFacade
    {
        private readonly IUserFacade _userService;
        private readonly IAccountConfigFacade _accountConfigFacade;
        private readonly DataContext _context;

        public AccountTransactionFacade(IUserFacade userService, IAccountConfigFacade accountConfigFacade,DataContext context)
        {
            _userService = userService;
            _accountConfigFacade = accountConfigFacade;
            _context = context;
        }

        public async Task SaveIncome(SingleIncomeDTO singleIncomeDTO)
        {
            SingleEarning singleEarning = new SingleEarning(singleIncomeDTO.Value, singleIncomeDTO.Name, singleIncomeDTO.Date);
            _context.TransactionHistories.Add(new TransactionHistory(singleEarning, _userService.GetCurrentUser().Result.Username));

            await _context.SaveChangesAsync();
        }

        public async Task SaveSpending(AccountSpendingDTO spendingDTO)
        {
            SpendingFactory spendingFactory = new SpendingFactory();
            Spending spending = spendingFactory.Create(spendingDTO);

            if (spending is SingleSpending singleSpending)
            {
                _context.TransactionHistories.Add(new TransactionHistory(singleSpending, _userService.GetCurrentUser().Result.Username));
            }
            if (spending is ConstantSpending constantSpending)
            {
                await _accountConfigFacade.AddNewConstantSpending(constantSpending);
            }    
            
            await _context.SaveChangesAsync();
        }

        public async Task<TransactionsBalanceDTO> GetTransactionsBalance()
        {
            List<TransactionHistory> transactions = await _context.TransactionHistories.ToListAsync();

            List<SingleEarning> incomes = transactions.Where(t => t.Type == TransactionType.INCOME).Select(i => new SingleEarning(i.Value, i.Description, i.Date)).ToList();
            List<SingleSpending> spendings = transactions.Where(t => t.Type == TransactionType.SPENDING).Select(i => new SingleSpending(i.Value, i.Description, i.Category ?? Category.OTHER, i.Date)).ToList();

            return new TransactionsBalanceDTO(incomes, spendings);
        }
    }
}
