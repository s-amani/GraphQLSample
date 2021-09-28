using System.Threading;
using System.Threading.Tasks;
using GraphQLSample.Core.Application.Services.Services.Contracts;
using MediatR;

namespace GraphQLSample.Core.Application.Services.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerService _service;

        public DeleteCustomerCommandHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken) =>
            await _service.DeleteAsync(request.Id);
    }
}
