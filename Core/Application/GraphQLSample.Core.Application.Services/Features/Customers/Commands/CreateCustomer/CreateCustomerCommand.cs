using GraphQLSample.Core.Application.Services.GraphTypes;
using GraphQLSample.Shared.Common.AutoMapper;
using MediatR;

namespace GraphQLSample.Core.Application.Services.Features.Customers.Commands.CreateCustomer
{
    [Map(MapTo = new[]
    {
        typeof(Infrastructure.Domain.Entities.Customer)
    })]
    public class CreateCustomerCommand : CustomerAddViewModel, 
        IRequest<GraphQLPayload<Infrastructure.Domain.Entities.Customer>>, IAutoMapMarker
    {
    }
}
