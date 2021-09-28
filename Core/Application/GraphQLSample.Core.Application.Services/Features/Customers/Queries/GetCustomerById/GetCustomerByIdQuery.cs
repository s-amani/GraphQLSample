using MediatR;

namespace GraphQLSample.Core.Application.Services.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<Infrastructure.Domain.Entities.Customer>
    {
        public long Id { get; private set; }

        public GetCustomerByIdQuery(long id)
        {
            Id = id;
        }
    }
}
