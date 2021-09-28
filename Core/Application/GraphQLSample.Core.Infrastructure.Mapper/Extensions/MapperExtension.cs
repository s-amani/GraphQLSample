using AutoMapper;
using GraphQLSample.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLSample.Core.Infrastructure.Mapper.Extensions
{
    public static class MapperExtension
    {
        public static TDestination Map<TDestination>(this object source, IHttpContextAccessor httpsAccessor)
        {
            var mapper = httpsAccessor.HttpContext.RequestServices.GetService<IMapper>();
            mapper.ThrowExceptionIfNull(nameof(mapper));
            return mapper.Map<TDestination>(source);
        }
    }
}
