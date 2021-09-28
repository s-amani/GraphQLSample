using GraphQLSample.Core.Application.Services.BaseViewModels;
using MediatR;

namespace GraphQLSample.Core.Application.Services.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : DeleteViewModel, IRequest<bool>
    {
    }
}
