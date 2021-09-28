using System.Collections.Generic;
using System.Linq;
using GraphQLSample.Core.Application.Services.Services.Contracts;
using GraphQLSample.Core.Infrastructure.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace GraphQLSample.Core.Application.Services.Services
{
    public class DbSeedService
    {
        public static void SeedAsync(ICustomerService customerService, ILogger<DbSeedService> logger)
        {
            if (customerService.Repository.Set.Any())
                return;

            customerService.AddRange(GetPreconfiguredCustomers());
            logger.LogInformation("Seeding database");
        }

        private static IEnumerable<Customer> GetPreconfiguredCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Name = "Chad", Email = "chad@example.com", Code = 1001,
                    Status = Infrastructure.Domain.Enums.Status.Active, IsBlocked = false
                },
                new Customer
                {
                    Name = "Jasmine", Email = "jasmine@example.com", Code = 1002,
                    Status = Infrastructure.Domain.Enums.Status.Inactive, IsBlocked = true
                }
            };
        }

    }
}
