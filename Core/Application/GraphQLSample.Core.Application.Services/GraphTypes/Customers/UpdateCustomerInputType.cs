using GraphQLSample.Core.Application.Services.Features.Customers.Commands.UpdateCustomer;
using HotChocolate.Types;

namespace GraphQLSample.Core.Application.Services.GraphTypes.Customers
{
    public class UpdateCustomerInputType : InputObjectType<UpdateCustomerCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UpdateCustomerCommand> descriptor)
        {
            descriptor.Description("Represents the model used to update existing customer");

            descriptor
                .Field(x => x.Name)
                .Description("Represents the name of customer");

            descriptor
                .Field(x => x.Email)
                .Description("Represents a unique email address for each customer");

            descriptor
                .Field(x => x.Code)
                .Description("Represents the customer code");

            descriptor
                .Field(x => x.Status)
                .Description("Represents the current status of the customer");

            descriptor
                .Field(x => x.IsBlocked)
                .Description("Represents if the customer banned or not");
        }
    }
}
