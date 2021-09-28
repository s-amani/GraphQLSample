using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLSample.Core.Application.Services.Services;
using GraphQLSample.Core.Application.Services.Services.Contracts;
using GraphQLSample.Core.Infrastructure.DataLayer.DAL;
using GraphQLSample.Web.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLSample.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<ApplicationDbContext>(services =>
                {
                    var logger = services.GetService<ILogger<DbSeedService>>();
                    var service = services.GetService<ICustomerService>();

                    DbSeedService.SeedAsync(service, logger);
                })
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
