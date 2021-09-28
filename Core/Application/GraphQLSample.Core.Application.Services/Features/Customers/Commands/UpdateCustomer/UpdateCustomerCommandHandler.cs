using System.Threading;
using System.Threading.Tasks;
using GraphQLSample.Core.Application.Services.GraphTypes;
using GraphQLSample.Core.Application.Services.Services.Contracts;
using MediatR;

namespace GraphQLSample.Core.Application.Services.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, GraphQLPayload<Infrastructure.Domain.Entities.Customer>>
    {
        private readonly ICustomerService _service;

        public UpdateCustomerCommandHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task<GraphQLPayload<Infrastructure.Domain.Entities.Customer>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken) =>
            await _service.EditAsync(request);
    }
}
