using System;
using GraphQLSample.Core.Application.IoC;
using GraphQLSample.Core.Application.Services.BaseViewModels;
using GraphQLSample.Core.Infrastructure.DataLayer.DAL;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLSample.Test
{
    public class TestComponentProvider
    {
        public static IServiceProvider GetServiceProvider()
        {
            const string connectionString =
                "Database=GQLSampleDB; Server=.\\SQLExpress; Integrated Security=SSPI; MultipleActiveResultSets=True;Max Pool Size=500;";

            return new ServiceCollection()
                    .AddPooledDbContextFactory<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(connectionString,
                            opt => opt.EnableRetryOnFailure());
                    })
                    .RegisterDependencies(typeof(DefaultIdViewModel))
                    .AddDefaultBatchDispatcher()
                    .BuildServiceProvider();
        }

        public static IRequestExecutor GetQueryExecutor<TQuery, TQueryType>() where TQuery : class
        {
            return Schema.Create(c =>
                {
                    c.RegisterType<TQueryType>();
                    c.RegisterQueryType<TQuery>();
                })
                .MakeExecutable();
        }

        public static IRequestExecutor GetMutationExecutor<TMutation, TQuery, TInputType, TQueryType>()
            where TMutation : class
            where TQuery : class
        {
            return Schema.Create(c =>
                {
                    c.RegisterType<TInputType>();
                    c.RegisterType<TQueryType>();
                    c.RegisterQueryType<TQuery>();
                    c.RegisterMutationType<TMutation>();
                })
                .MakeExecutable();
        }

        public static IReadOnlyQueryRequest GetRequest(string query, IServiceProvider services) => QueryRequestBuilder.New()
            .SetQuery(query)
            .SetAllowedOperations(new[] { OperationType.Mutation, OperationType.Query })
            .SetServices(services)
            .Create();
    }
}
