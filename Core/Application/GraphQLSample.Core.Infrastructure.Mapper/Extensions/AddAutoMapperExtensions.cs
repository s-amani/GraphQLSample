using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLSample.Core.Infrastructure.Mapper.Extensions
{
    public static class AddAutoMapperExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var profiles =
                from t in typeof(AutoMapperRegistryProfile).Assembly.GetTypes()
                where typeof(Profile).IsAssignableFrom(t)
                select (Profile)Activator.CreateInstance(t);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }

                AutoMapperDetector.Map(cfg);
            });

            services.AddScoped<IMapper>(factory => factory.GetService<MapperConfiguration>()?.CreateMapper());
            services.AddScoped<MapperConfiguration>(factory => config);
        }
    }
}
