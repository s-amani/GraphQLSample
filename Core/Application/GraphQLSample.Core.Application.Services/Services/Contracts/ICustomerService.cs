using GraphQLSample.Core.Application.Services.BaseServices;
using GraphQLSample.Core.Application.Services.Features.Customers.Commands.CreateCustomer;
using GraphQLSample.Core.Application.Services.Features.Customers.Commands.UpdateCustomer;
using GraphQLSample.Core.Application.Services.Features.Customers.Queries.GetCustomerById;
using GraphQLSample.Core.Infrastructure.Domain.Entities;
using GraphQLSample.Shared.Common;

namespace GraphQLSample.Core.Application.Services.Services.Contracts
{
    public interface ICustomerService : IServiceBase<long, Customer, CustomerViewModel, CustomerAddViewModel, CustomerEditViewModel>, IInjectable
    {
    }
}
