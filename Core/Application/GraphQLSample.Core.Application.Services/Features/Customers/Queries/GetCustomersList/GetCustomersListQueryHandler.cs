using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GraphQLSample.Core.Application.Services.Services.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GraphQLSample.Core.Application.Services.Features.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, IReadOnlyList<Infrastructure.Domain.Entities.Customer>>
    {
        private readonly ICustomerService _service;

        public GetCustomersListQueryHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task<IReadOnlyList<Infrastructure.Domain.Entities.Customer>> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            return await _service.Repository.FindAll().Include(x=> x.Addresses).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}