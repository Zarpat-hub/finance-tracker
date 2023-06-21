using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WealthApi.Core.Enums;

namespace WealthApi.Core
{
    public class AccountSpending 
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("category")]
        public List<Category> Category { get; set; }

    }

    public AccountSpending()
    {

    }

    public AccountSpending(string type, string date, int value, string description, List<Category> category)
    {
        Type = type;
        date = date;
        value = value;
        description = description;
        Category = category;
    }
}
