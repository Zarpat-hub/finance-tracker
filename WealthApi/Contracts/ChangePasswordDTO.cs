using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WealthApi.Contracts
{
    public class ChangePasswordDTO
    {
        [JsonProperty]
        [Required]
        public string OldPassword { get; set; }

        [JsonProperty]
        [Required]
        public string NewPassword { get; set; }

        [JsonProperty]
        [Required]
        public string RetypedNewPassword { get; set; }

    }
}
