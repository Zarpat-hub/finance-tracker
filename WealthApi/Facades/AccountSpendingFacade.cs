using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WealthApi.Core;
using WealthApi.Database;
using WealthApi.Database.Models;

namespace WealthApi.Facades
{
    public interface IAccountSpendingFacade
    {
        Task SaveSpending(IAccountSpendingFacade spending);
        Task<IAccountSpendingFacade> GetAccountSpending();
    }

    public class AccountSepndingFacade : IAccountSpendingFacade
    {
        private readonly IUserFacade _userService;
        private readonly DataContext _context;

        public AccountSepndingFacade(IUserFacade userService, DataContext context)
        {
            _userService = userService;
            _context = context;
        }

        public Task<IAccountSpendingFacade> GetSpending()
        {
            throw new NotImplementedException();
        }

        public async Task SaveSpending(IAccountSpendingFacade spending)
        {
            var accountSpending = new AccountSpending
            {
                Type = spending.Type,
                Frequence = spending.Frequence,
                Date = spending.Date,
                Value = spending.Value,
                Description = spending.Description,
                Category = spending.Category
            };

            _context.TransactionHistory.Add(accountSpending);
            await _context.SaveChangesAsync();
        }
    }
}
