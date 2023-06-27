using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WealthApi.Contracts
{
    public class EditProfileDTO
    {
        [JsonProperty]
        [Required]
        public string Username { get; set; }

        [JsonProperty]
        [Required]
        public string Email { get; set; }

        [JsonProperty]
        [Required]
        public string? Firstname { get; set; }

        [JsonProperty]
        [Required]
        public string? Lastname { get; set; }

    }
}
