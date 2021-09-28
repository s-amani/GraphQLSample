using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLSample.Core.Application.Services.Features.Customers.Commands.CreateCustomer;
using HotChocolate.Types;

namespace GraphQLSample.Core.Application.Services.GraphTypes.Customers
{
    public class CreateCustomerInputType : InputObjectType<CreateCustomerCommand>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CreateCustomerCommand> descriptor)
        {
            descriptor.Description("Represents the model used to create new customer"); 

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
