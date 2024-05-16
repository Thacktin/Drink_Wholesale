using Microsoft.EntityFrameworkCore;

namespace Drink_Wholesale.Models
{
    public class DrinkWholesaleDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<SubCategory> SubCategories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

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
