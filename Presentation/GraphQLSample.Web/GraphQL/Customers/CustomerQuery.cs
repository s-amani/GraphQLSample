using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQLSample.Core.Application.Services.Features.Customers.Queries.GetCustomersList;
using GraphQLSample.Core.Infrastructure.DataLayer.DAL;
using GraphQLSample.Core.Infrastructure.Domain.Entities;
using HotChocolate;
using HotChocolate.Data;
using MediatR;

namespace GraphQLSample.Web.GraphQL.Customers
{
    public class CustomerQuery
    {
        public async Task<IReadOnlyList<Customer>> GetCustomer([Service] IMediator mediator) => 
            await mediator.Send(new GetCustomersListQuery());
    }
}
