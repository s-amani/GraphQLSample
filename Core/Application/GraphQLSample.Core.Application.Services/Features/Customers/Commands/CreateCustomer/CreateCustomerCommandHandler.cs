using System.Threading;
using System.Threading.Tasks;
using GraphQLSample.Core.Application.Services.GraphTypes;
using GraphQLSample.Core.Application.Services.Services.Contracts;
using MediatR;

namespace GraphQLSample.Core.Application.Services.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : 
        IRequestHandler<CreateCustomerCommand, 
            GraphQLPayload<Infrastructure.Domain.Entities.Customer>>
    {
        private readonly ICustomerService _service;

        public CreateCustomerCommandHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task<GraphQLPayload<Infrastructure.Domain.Entities.Customer>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken) => await _service.AddAsync(request);
    }
}
