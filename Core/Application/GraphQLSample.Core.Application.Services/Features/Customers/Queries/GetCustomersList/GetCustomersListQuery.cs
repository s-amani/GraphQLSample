using System.Collections.Generic;
using MediatR;

namespace GraphQLSample.Core.Application.Services.Features.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQuery : IRequest<IReadOnlyList<Infrastructure.Domain.Entities.Customer>>
    {
    }
}
