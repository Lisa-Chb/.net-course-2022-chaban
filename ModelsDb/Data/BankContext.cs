using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ModelsDb.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client_db> Clients { get; set; }
        public DbSet<Employee_db> Employees { get; set; }
        public DbSet<Account_db> Accounts { get; set; }
        public DbSet<Currency_db> Currency { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Superliza228");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account_db>()
                .HasOne(b => b.Currency)
                .WithOne(i => i.Account)
                .HasForeignKey<Currency_db>(b => b.AccountId);
        }
    }
}

