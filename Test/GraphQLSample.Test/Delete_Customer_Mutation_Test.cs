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
    public class DeleteCustomerMutationTest
    {
        [Test]
        public void ShouldDeleteCustomer()
        {
            var services = TestComponentProvider.GetServiceProvider();
            var executor = TestComponentProvider.GetMutationExecutor<CustomerMutation,
                CustomerQuery, CreateCustomerInputType, CustomerType>();

            var request = TestComponentProvider.GetRequest(@"
                mutation {
                  deleteCustomer(input: {id: 9}) 
                }
            ", services);

            var result = executor.Execute(request).ToJson();

            Approvals.Verify(result);
        }
    }
}
