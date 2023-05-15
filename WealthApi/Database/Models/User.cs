using System.ComponentModel.DataAnnotations;

namespace WealthApi.Database.Models
{
    public class User
    {
        [Key]
        public string Username { get; init; }
        public string Email { get; init; }
        public string EncryptedPassword { get; init; }
        public string? RegistrationVerificationToken { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
