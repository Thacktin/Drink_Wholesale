using Drink_Wholesale.Persistence;
using Drink_Wholesale.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Drink_Wholesale.Persistence.Models;
using Microsoft.AspNetCore.Identity;

namespace Drink_Wholesale.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DrinkWholesaleDbContext>(options =>
            {
                IConfigurationRoot configuration = builder.Configuration;
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
                options.UseLazyLoadingProxies();
            });
            // Add services to the container.
            builder.Services.AddTransient<IDrinkWholesaleService, DrinkWholesaleService>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<DrinkWholesaleDbContext>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
