using HotChocolate.Types;

namespace GraphQLSample.Core.Application.Services.GraphTypes.Customers
{
    public class CustomerType : ObjectType<Infrastructure.Domain.Entities.Customer>
    {
        protected override void Configure(IObjectTypeDescriptor<Infrastructure.Domain.Entities.Customer> descriptor)
        {
            descriptor.Description("Represent a customer information");


            descriptor
                .Field(x => x.Email)
                .Description("Represents a unique email address for each customer")
                .Ignore();

            descriptor
                .Field(x => x.Addresses)
                .Description("This is the list of customer addresses");
        }
    }
}
