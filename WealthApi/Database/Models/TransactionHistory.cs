using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WealthApi.Database.Models
{
    public class TransactionHistory
    {
        [Key]
        public string Username { get; set; }
        [ForeignKey("Username")]
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }

}
