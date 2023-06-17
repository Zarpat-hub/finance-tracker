using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WealthApi.Database.Models
{
    public class AccountConfiguration
    {
        [Key]
        public string Username { get; set; }
        [ForeignKey("Username")]
        public User User { get; set; }
        public string ConfigurationJson { get; set; }

        public AccountConfiguration(string username, string configurationJson)
        {
            Username = username;
            ConfigurationJson = configurationJson;
        }
    }
}
