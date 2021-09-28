using System;
using GraphQLSample.Core.Infrastructure.DataLayer.DAL;
using GraphQLSample.Shared.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace GraphQLSample.Web.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<IServiceProvider> seeder) where TContext : ApplicationDbContext
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            var logger = services.GetRequiredService<ILogger<TContext>>();
            logger.ThrowExceptionIfNull(nameof(logger));

            var context = services.GetService<TContext>();
            context.ThrowExceptionIfNull(nameof(context));

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                var retry = Policy
                    .Handle<SqlException>()
                    .WaitAndRetry(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        (exception, retryCount, context) =>
                        {
                            logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                        });

                retry.Execute(() => InvokeSeeder(seeder, context, services));

                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<IServiceProvider> seeder, TContext context, IServiceProvider services)
            where TContext : ApplicationDbContext
        {
            context.Database.Migrate();
            seeder(services);
        }
    }
}
