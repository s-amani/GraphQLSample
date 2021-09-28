using ApprovalTests;
using ApprovalTests.Reporters;
using GraphQLSample.Core.Application.Services.GraphTypes.Customers;
using GraphQLSample.Web.GraphQL.Customers;
using HotChocolate;
using HotChocolate.Execution;
using NUnit.Framework;

namespace GraphQLSample.Test
{
    [UseReporter(typeof(DiffReporter))]
    public class CustomerQueryTest
    {
        [Test]
        public void ShouldReturnCustomers()
        {
            var services = TestComponentProvider.GetServiceProvider();
            var executor = TestComponentProvider.GetQueryExecutor<CustomerQuery, CustomerType>();

            var request = TestComponentProvider.GetRequest(@"
                { 
                    customer {
                        name
                        code
                        status
                        isBlocked
                        createdAt
                        modifiedAt
                    }
                }
            ", services);

            var result = executor.Execute(request).ToJson();

            Approvals.Verify(result);
        }
    }
}