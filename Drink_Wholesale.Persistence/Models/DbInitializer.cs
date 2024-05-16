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
                    Name = "Alkoholos"
                },
                new Category()
                {
                    Name = "Alkoholmentes"
                }
            };
            dbContext.AddRange(categories);
            dbContext.SaveChanges();

            IList<SubCategory> subCategories = new List<SubCategory>
            {
                new SubCategory()
                {
                    Name = "Sörök",
                    CategoryId = 1
                },
                new SubCategory()
                {
                    Name = "Borok",
                    CategoryId = 1
                },
                new SubCategory()
                {
                    Name = "Üdítők",
                    CategoryId = 2
                },
                new SubCategory()
                {
                    Name = "Alkoholmentes sörök",
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
                    NetPrice = 340,
                    Packaging = Packaging.Single | Packaging.ShrinkWrap| Packaging.Crate | Packaging.Tray,
                    SubCategoryId = 1,
                    Producer = "Heineken Hungária kft"
                },
                new Product()
                {
                    ArtNo = 1246,
                    Description = "Borsodi",
                    Inventory = 88,
                    NetPrice = 280,
                    Packaging = Packaging.Single | Packaging.ShrinkWrap |  Packaging.Tray,
                    SubCategoryId = 1,
                    Producer = "Borsodi sörgyár kft"
                },
                new Product()
                {
                    ArtNo = 1247,
                    Description = "Arany fácán",
                    Inventory = 36,
                    NetPrice = 250,
                    Packaging = Packaging.Single | Packaging.ShrinkWrap| Packaging.Crate | Packaging.Tray,
                    SubCategoryId = 1,
                    Producer = "Heineken Hungária kft"
                },
                new Product()
                {
                    ArtNo = 1248,
                    Description = "Arany fácán2",
                    Inventory = 36,
                    NetPrice = 250,
                    Packaging = Packaging.Single | Packaging.ShrinkWrap| Packaging.Crate | Packaging.Tray,
                    SubCategoryId = 1,
                    Producer = "Heineken Hungária kft"
                },
                new Product()
                {
                    ArtNo = 1249,
                    Description = "Arany fácán3",
                    Inventory = 36,
                    NetPrice = 250,
                    Packaging = Packaging.Single | Packaging.ShrinkWrap| Packaging.Crate | Packaging.Tray,
                    SubCategoryId = 1,
                    Producer = "Heineken Hungária kft"
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
                    ArtNo = 1256,
                    Description = "Dankó koccintós vörös bor",
                    Inventory = 45,
                    NetPrice = 550,
                    Packaging = Packaging.Single |Packaging.Crate,
                    SubCategoryId = 2,
                    Producer = "Weinhaus kft"
                },
                new Product()
                {
                    ArtNo = 1286,
                    Description = "Coca Cola 1.75l",
                    Inventory = 90,
                    NetPrice = 500,
                    Packaging = Packaging.ShrinkWrap | Packaging.Single,
                    SubCategoryId = 3,
                    Producer = "Coca Cola hfb"
                },
                new Product()
                {
                    ArtNo = 1287,
                    Description = "Fante Zero 1.75l",
                    Inventory = 100,
                    NetPrice = 480,
                    Packaging = Packaging.ShrinkWrap | Packaging.Single,
                    SubCategoryId = 3,
                    Producer = "Coca Cola hfb"
                },
                new Product()
                {
                    ArtNo = 2145,
                    Description = "Soproni 0.0%",
                    Inventory = 24,
                    Packaging = Packaging.Tray | Packaging.Crate,
                    SubCategoryId = 4,
                    Producer = "Heineken hungary"
                },
                new Product()
                {
                    ArtNo = 2145,
                    Description = "Borsodi 0.0%",
                    Inventory = 24,
                    Packaging = Packaging.Tray | Packaging.Crate,
                    SubCategoryId = 4,
                    Producer = "Borsodi sörfőzde"
                }
            };
            dbContext.AddRange(products);
            dbContext.SaveChanges();
        }
    }
}
