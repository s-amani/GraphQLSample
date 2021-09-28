using System;
using System.Linq;
using System.Reflection;
using GraphQLSample.Shared.Common;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLSample.Core.Application.IoC
{
    public static class DependencyRegistery
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services, Type objectType)
        {
            var query = 
                from 
                    t in objectType.Assembly.GetTypes()
                where 
                    t.GetTypeInfo().IsClass && 
                    !t.GetTypeInfo().IsAbstract && 
                    t.GetTypeInfo().ImplementedInterfaces.Any(i => i == typeof(IInjectable))
                select t;

            var orderedQuery = query.OrderBy(p => p.Name).ToList();

            foreach (var type in orderedQuery)
            {
                Type interfaceType = null;

                foreach (var it in type.GetInterfaces())
                {
                    if (it.GetInterfaces().All(i => i != typeof(IInjectable))) 
                        continue;
                    
                    interfaceType = it;
                }

                services.AddScoped(interfaceType, type);
            }

            services.AddCustomServices();

            return services;
        }
    }
}
