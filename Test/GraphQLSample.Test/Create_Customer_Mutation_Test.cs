using ApprovalTests;
using ApprovalTests.Reporters;
using GraphQLSample.Core.Application.Services.GraphTypes.Customers;
using GraphQLSample.Core.Infrastructure.Domain.Entities;
using GraphQLSample.Web.GraphQL.Customers;
using HotChocolate;
using HotChocolate.Execution;
using NUnit.Framework;

namespace GraphQLSample.Test
{
    [UseReporter(typeof(DiffReporter))]
    public class CreateCustomerMutationTest
    {
        [Test]
        public void ShouldCreateCustomer()
        {
            var services = TestComponentProvider.GetServiceProvider();
            var executor = TestComponentProvider.GetMutationExecutor<CustomerMutation,
                CustomerQuery, CreateCustomerInputType, CustomerType>();

            var request = TestComponentProvider.GetRequest(@"
                mutation { 
                    createCustomer(input: {
                            name: ""Reza Rahmati"", 
                            email: ""roya@example.com"", 
                            status: ACTIVE, 
                            isBlocked: true}) {
                        payload {
                            name
                        }
                    }
                }
            ", services);

            var result = executor.Execute(request).ToJson();

            Approvals.Verify(result);
        }
    }
}
