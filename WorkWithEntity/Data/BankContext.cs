using Microsoft.EntityFrameworkCore;
using ModelsDb;


namespace WorkWithEntity.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client_db> Clients { get; set; }
        public DbSet<Employee_db> Employees { get; set; }
        public DbSet<Account_db> Accounts { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Superliza228");
        }
    }
}

