
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class ApplicationContext : DbContext
    {
        public DbSet<PriceItem> PriceItems => Set<PriceItem>();

        public ApplicationContext()
        {
            
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "PriceItems.db" };
            //var connectionString = connectionStringBuilder.ToString();
            //var connection = new SqliteConnection(connectionString);

            //optionsBuilder.UseSqlite(connection);
            

            //optionsBuilder.UseNpgsql("DefaultConnection");
        }
    }
}
