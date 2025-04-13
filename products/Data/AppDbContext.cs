using Microsoft.EntityFrameworkCore;
using products.Models;

namespace products.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("PostgresConn"));
        }
        
        public DbSet<ProductEntry> ProductEntries { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}
