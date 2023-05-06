using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WealthApi.Contracts
{
    public class UserLoginDTO
    {
        [JsonProperty]
        [Required]
        public string Username { get; set; }

        [JsonProperty]
        [Required]
        public string Password { get; set; }
    }
}
