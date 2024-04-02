using Drink_Wholesale.Servicies;
using Microsoft.EntityFrameworkCore;

namespace Drink_Wholesale.Models
{
    public class DrinkWholesaleDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(connectionString: "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DrinkWholesale;Trusted_Connection=True;MultipleActiveResultSets=True");
        //    base.OnConfiguring(optionsBuilder);
        //}
        public DrinkWholesaleDbContext(DbContextOptions<DrinkWholesaleDbContext> builder) : base(builder)
        {
            
        }
    }
}
