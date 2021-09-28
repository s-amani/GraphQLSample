using System;
using System.Threading;
using System.Threading.Tasks;
using GraphQLSample.Core.Application.Services.Services.Contracts;
using MediatR;

namespace GraphQLSample.Core.Application.Services.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, 
        Infrastructure.Domain.Entities.Customer>
    {
        private readonly ICustomerService _service;

        public GetCustomerByIdQueryHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task<Infrastructure.Domain.Entities.Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.Repository.FindAsync(x => x.Id == request.Id);
        }
    }
}
