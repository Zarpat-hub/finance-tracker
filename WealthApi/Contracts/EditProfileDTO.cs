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

    }
}
