using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WealthApi.Core;
using WealthApi.Core.Enums;

namespace WealthApi.Database.Models
{
    public class TransactionHistory
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        [ForeignKey("Username")]
        public User User { get; set; }
        public string Date { get; set; }
        public int Value { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
        public Category? Category { get; set; }

        public TransactionHistory() { }

        public TransactionHistory(SingleSpending spending, string username)
        {
            Username = username;
            Date = spending.Date;
            Value = spending.Value;
            Description = spending.Description;
            Category = spending.Category;
            Type = TransactionType.SPENDING;
        }

        public TransactionHistory(SingleEarning earning, string username)
        {
            Username = username;
            Date = earning.Date;
            Value = earning.Value;
            Description = earning.Name;
            Type = TransactionType.INCOME;
        }
    }
}
