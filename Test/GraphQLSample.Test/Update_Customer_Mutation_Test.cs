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
    public class UpdateCustomerMutationTest
    {
        [Test]
        public void ShouldUpdateCustomer()
        {
            var services = TestComponentProvider.GetServiceProvider();
            var executor = TestComponentProvider.GetMutationExecutor<CustomerMutation,
                CustomerQuery, UpdateCustomerInputType, CustomerType>();

            var request = TestComponentProvider.GetRequest(@"
                mutation {
                  updateCustomer(input: {
                        id: 1, 
                        name: ""Chad Ormond"", 
                        email: ""saber@example.com"", 
                        status: ACTIVE, 
                        isBlocked: true}) {
                        payload {
                            name
                            status
                        }
                    }
                }
            ", services);
                

            var result = executor.Execute(request).ToJson();

            Approvals.Verify(result);
        }
    }
}
