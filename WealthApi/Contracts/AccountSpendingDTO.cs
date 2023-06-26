using Newtonsoft.Json;
using WealthApi.Core.Enums;

namespace WealthApi.Core
{
    public class AccountSpendingDTO
    {
        [JsonProperty("type")]
        public SpendingType Type { get; set; }
        [JsonProperty("date", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string? Date { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("category")]
        public Category Category { get; set; }
        [JsonProperty("frequence", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public Frequence Frequence { get; set; }
    }
}
