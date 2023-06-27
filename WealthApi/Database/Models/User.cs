using System.ComponentModel.DataAnnotations;

namespace WealthApi.Database.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string Email { get; set; }
        public string EncryptedPassword { get; set; }
        public string? RegistrationVerificationToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserImg { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<AccountConfiguration> AccountsConfigurations { get; set; }
    }
}
