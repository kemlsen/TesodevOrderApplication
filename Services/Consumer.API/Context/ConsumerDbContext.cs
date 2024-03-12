using Consumer.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Consumer.API.Context
{
    public class ConsumerDbContext : DbContext
    {
        public DbSet<Audit> Audits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost; Database=ConsumerAPIDb; Port=5432; User Id=postgres; Password=123456; Timeout=30; Command Timeout=30;");
        }
    }
}
