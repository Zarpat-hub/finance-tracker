using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WealthApi.Core.Enums;

namespace WealthApi.Contracts
{
    public class NewGoalDTO
    {
        [JsonProperty]
        [Required]
        public string Name { get; set; }

        [JsonProperty]
        [Required]
        public int Value { get; set; }

        [JsonProperty]
        [Required]
        public Priority Priority { get; set; }

        [JsonProperty]
        [Required]
        public string Deadline { get; set; }

    }
}
