using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace ModelsDb.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ClientDb> Clients { get; set; }
        public DbSet<EmployeeDb> Employees { get; set; }
        public DbSet<AccountDb> Accounts { get; set; }
        public DbSet<CurrencyDb> Currency { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Superliza228");
            optionsBuilder.LogTo(s => Debug.WriteLine(s));
        }
    }
}

