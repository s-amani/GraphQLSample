using GraphQLSample.Core.Application.Services.BaseViewModels;
using GraphQLSample.Core.Infrastructure.DataLayer.DAL;
using GraphQLSample.Core.Infrastructure.DataLayer.Repositories;
using GraphQLSample.Core.Infrastructure.Mapper.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLSample.Core.Application.IoC
{
    internal static class CustomServices
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddLogging();
            services.AddAutoMapper();
            services.AddMediatR(typeof(ApiIdViewModel<>).Assembly);
            services.AddScoped<IUnitOfWork, ApplicationDbContext>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        }
    }
}
