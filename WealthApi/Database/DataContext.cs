using Microsoft.EntityFrameworkCore;
using WealthApi.Database.Models;

namespace WealthApi.Database
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _config;

        public DataContext(IConfiguration configuration)
        {
            _config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(_config.GetConnectionString("WEALTH_LOCAL_POSTGRE"));
            options.EnableSensitiveDataLogging().LogTo(Console.WriteLine);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AccountConfiguration> AccountsConfigurations { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
    }
}
