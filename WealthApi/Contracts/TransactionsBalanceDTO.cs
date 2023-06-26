using WealthApi.Core;

namespace WealthApi.Contracts
{
    public class TransactionsBalanceDTO
    {
        public List<SingleEarning> Incomes { get; init; }
        public List<SingleSpending> Spendings { get; init; }

        public TransactionsBalanceDTO(List<SingleEarning> incomes, List<SingleSpending> spendings)
        {
            Incomes = incomes;
            Spendings = spendings;
        }
    }
}
