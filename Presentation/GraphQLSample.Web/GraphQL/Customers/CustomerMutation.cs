using System.Threading.Tasks;
using GraphQLSample.Core.Application.Services.Features.Customers.Commands.CreateCustomer;
using GraphQLSample.Core.Application.Services.Features.Customers.Commands.DeleteCustomer;
using GraphQLSample.Core.Application.Services.Features.Customers.Commands.UpdateCustomer;
using GraphQLSample.Core.Application.Services.GraphTypes;
using GraphQLSample.Core.Infrastructure.Domain.Entities;
using HotChocolate;
using MediatR;

namespace GraphQLSample.Web.GraphQL.Customers
{
    public class CustomerMutation
    {
        public async Task<GraphQLPayload<Customer>> CreateCustomer([Service] IMediator mediator, CreateCustomerCommand input) =>
            await mediator.Send(input);


        public async Task<GraphQLPayload<Customer>> UpdateCustomer([Service] IMediator mediator, UpdateCustomerCommand input) =>
            await mediator.Send(input);


        public async Task<bool> DeleteCustomer([Service] IMediator mediator, DeleteCustomerCommand input) =>
            await mediator.Send(input);
    }
}
