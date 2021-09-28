using GraphQLSample.Core.Application.Services.GraphTypes;
using GraphQLSample.Shared.Common.AutoMapper;
using MediatR;

namespace GraphQLSample.Core.Application.Services.Features.Customers.Commands.UpdateCustomer
{
    [Map(
        MapTo = new[]
        {
            typeof(Infrastructure.Domain.Entities.Customer)
        },
        MapFrom = new[]
        {
            typeof(Infrastructure.Domain.Entities.Customer)
        })]
    public class UpdateCustomerCommand : CustomerEditViewModel, IRequest<GraphQLPayload<Infrastructure.Domain.Entities.Customer>>, IAutoMapMarker
    {
    }
}
