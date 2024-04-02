using Microsoft.EntityFrameworkCore;

namespace Drink_Wholesale.Models
{
    public static class DbInitializer
    {
        public static void Initialize(DrinkWholesaleDbContext dbContext)
        {
            dbContext.Database.Migrate();

            if (dbContext.Categories.Any())
            {
                return;
            }

            IList<Category> categories = new List<Category>
            {
                new Category()
                {
                    Name = "Alcoholic"
                },
                new Category()
                {
                    Name = "Non-alcoholic"
                }
            };
            dbContext.AddRange(categories);
            dbContext.SaveChanges();

            IList<SubCategory> subCategories = new List<SubCategory>
            {
                new SubCategory()
                {
                    Name = "Beers",
                    CategoryId = 1
                },
                new SubCategory()
                {
                    Name = "Wines",
                    CategoryId = 1
                },
                new SubCategory()
                {
                    Name = "Soft Drinks",
                    CategoryId = 2
                },
                new SubCategory()
                {
                    Name = "Non-Alcoholic beers",
                    CategoryId = 2
                }
            };
            dbContext.AddRange(subCategories);
            dbContext.SaveChanges();

            IList<Product> products = new List<Product>
            {
                new Product()
                {
                    ArtNo = 1245,
                    Description = "Heineken",
                    Inventory = 132,
                    NetPrice = 3400,
                    Packaging = Packaging.Single | Packaging.ShrinkWrap| Packaging.Crate | Packaging.Tray,
                    SubCategoryId = 1,
                    Producer = "Heineken Hungária kft"
                },
                new Product()
                {
                    ArtNo = 1256,
                    Description = "Borsodi sör 0,5l",
                    Inventory = 99,
                    NetPrice = 280,
                    Packaging = Packaging.Single | Packaging.ShrinkWrap | Packaging.Tray,
                    SubCategoryId = 1,
                    Producer = "Borsodi Hungária kft"
                },
                new Product()
                {
                    ArtNo = 5869,
                    Description = "Arany fácán",
                    Inventory = 56,
                    NetPrice = 3400,
                    Packaging = Packaging.Single ,
                    SubCategoryId = 1,
                    Producer = "kft"
                },
                new Product()
                {
                    ArtNo = 1255,
                    Description = "Dankó koccintós fehér bor",
                    Inventory = 45,
                    NetPrice = 450,
                    Packaging = Packaging.Single |Packaging.ShrinkWrap,
                    SubCategoryId = 2,
                    Producer = "Weinhaus kft"
                },
                new Product()
                {
                    ArtNo = 1286,
                    Description = "Coca Cola 1.75l",
                    Inventory = 100,
                    NetPrice = 500,
                    Packaging = Packaging.ShrinkWrap | Packaging.Single,
                    SubCategoryId = 3,
                    Producer = "Coca Cola hfb"
                }
            };
            dbContext.AddRange(products);
            dbContext.SaveChanges();
        }
    }
}
