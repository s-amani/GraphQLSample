using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQLSample.Core.Application.IoC;
using GraphQLSample.Core.Application.Services.BaseViewModels;
using GraphQLSample.Core.Application.Services.GraphTypes.Customers;
using GraphQLSample.Core.Infrastructure.DataLayer.DAL;
using GraphQLSample.Web.Filters;
using GraphQLSample.Web.GraphQL.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace GraphQLSample.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("Default"), 
                    opt => opt.EnableRetryOnFailure());
            });
            
            services
                .AddGraphQLServer()
                .AddType<CustomerType>()
                .AddType<CreateCustomerInputType>()
                .AddType<UpdateCustomerInputType>()
                .AddMutationType<CustomerMutation>()
                .AddQueryType<CustomerQuery>()
                .AddErrorFilter<GraphErrorFilter>()
                .AddFiltering()
                .AddSorting();

            services.RegisterDependencies(typeof(DefaultIdViewModel));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/GQL");
            });

            app.UseGraphQLVoyager(new VoyagerOptions
            {
                GraphQLEndPoint = "/GQL"
            }, "/graphql-voyager");
        }
    }
}
