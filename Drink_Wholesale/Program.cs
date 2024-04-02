using Drink_Wholesale.Models;
using Drink_Wholesale.Services;
using Microsoft.EntityFrameworkCore;

namespace Drink_Wholesale
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using var context = new DrinkWholesaleDbContext();
            //DbInitializer.Initialize(context);
            //var service = new DrinkWholesaleService(context);
            //foreach (var category in service.GetCategories())
            //{
            //    Console.WriteLine(category.Id + " "+ category.Name);
            //}

            //foreach (var subCategory in service.GetSubCategories())
            //{
            //    Console.WriteLine(subCategory.Name +" "  + subCategory.Category.Name);
            //}

            //foreach (var product in service.GetAllProducts())
            //{
            //    Console.WriteLine(product.Description + " "+ product.Id + " " + product.SubCategory.Name);
            //}
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DrinkWholesaleDbContext>(options =>
            {


                IConfigurationRoot configuration = builder.Configuration;
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
                options.UseLazyLoadingProxies();
            });
            //Adding ORM services with DI
            builder.Services.AddTransient<IDrinkWholesaleService, DrinkWholesaleService>();

            //session handling
            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var serviceScope = app.Services.CreateScope())
            using (var context = serviceScope.ServiceProvider.GetRequiredService<DrinkWholesaleDbContext>())
            {
                string? imageSource = app.Configuration.GetValue<string>("ImageSource");
                DbInitializer.Initialize(context);
            }
            app.Run();
        }
    }
}
