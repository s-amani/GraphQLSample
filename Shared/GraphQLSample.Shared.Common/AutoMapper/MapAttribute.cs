using System;

namespace GraphQLSample.Shared.Common.AutoMapper
{
    public class MapAttribute : Attribute
    {
        public Type[] MapTo { get; set; }
        public Type[] MapFrom { get; set; }
    }
}
