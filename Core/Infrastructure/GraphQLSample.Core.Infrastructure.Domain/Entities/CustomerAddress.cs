using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLSample.Core.Infrastructure.Domain.BaseEntities;

namespace GraphQLSample.Core.Infrastructure.Domain.Entities
{
    public class CustomerAddress : DefaultEntityBase
    {
        public string Address { get; set; }

        public long CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
