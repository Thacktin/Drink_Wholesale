using Drink_Wholesale.Models;
using Drink_Wholesale.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
