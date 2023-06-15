using Newtonsoft.Json;

namespace WealthApi.Core
{
    public class AccountConfig
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("balance")]
        public int Balance { get; set; }

        [JsonProperty("payDay")]
        public int PayDay { get; set; }

        [JsonProperty("earnings")]
        public List<Earning> Earnings { get; set; }

        [JsonProperty("constantSpendings")]
        public List<ConstantSpending> ConstantSpendings { get; set; }
        
        [JsonProperty("spendingLimit")]
        public int SpendingLimit { get; set; }

        [JsonProperty("goals")]
        public List<Goal> Goals { get; set; }


        public AccountConfig()
        {

        }

        public AccountConfig(string currency, int balance, int payDay, List<Earning> earnings, List<ConstantSpending> constantSpendings, int spendingLimit, List<Goal> goals)
        {
            Currency = currency;
            Balance = balance;
            PayDay = payDay;
            Earnings = earnings;
            ConstantSpendings = constantSpendings;
            SpendingLimit = spendingLimit;
            Goals = goals;
        }
    }
}
