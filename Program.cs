using ClayTestCase.Data;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ContactManager.Data;

namespace ClayTestCase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Create Host builder
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    //Create Database
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();

                    //Get the list of users as strings
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    var userList = config.GetSection("userList").Get<List<string>>();

                    //Initialize database with users
                    SeedData.Initialize(services, userList).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred adding users.");
                }
            }
            host.Run();
        }

        //Build the host
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
