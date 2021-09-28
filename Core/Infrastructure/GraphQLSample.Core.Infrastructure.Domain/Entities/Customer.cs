using System.Collections.Generic;
using GraphQLSample.Core.Infrastructure.Domain.BaseEntities;
using GraphQLSample.Core.Infrastructure.Domain.Enums;

namespace GraphQLSample.Core.Infrastructure.Domain.Entities
{
    public class Customer : DefaultEntityBase
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


        public virtual ICollection<CustomerAddress> Addresses { get; set; }
    }
}
