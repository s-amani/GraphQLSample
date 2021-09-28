using GraphQLSample.Core.Application.Services.BaseViewModels;
using GraphQLSample.Core.Infrastructure.Domain.Enums;
using GraphQLSample.Shared.Common.AutoMapper;

namespace GraphQLSample.Core.Application.Services.Features.Customers.Queries.GetCustomerById
{
    [Map(MapFrom = new[]
    {
        typeof(Infrastructure.Domain.Entities.Customer)
    })]
    public class CustomerViewModel : DefaultLongViewModel, IAutoMapMarker
    {
        /// <summary>
        /// Get or set the customer email
        /// </summary>
        public string Email { get; set; }

        public string Name { get; set; }

        public int? Code { get; set; }

        /// <summary>
        /// Get or set the current Status of the Customer
        /// </summary>
        public Status Status { get; set; }

        public bool IsBlocked { get; set; }

    }


}
