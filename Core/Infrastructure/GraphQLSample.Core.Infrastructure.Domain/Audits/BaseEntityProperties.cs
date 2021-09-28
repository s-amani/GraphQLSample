using System;
using System.Linq;
using GraphQLSample.Core.Infrastructure.Domain.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GraphQLSample.Core.Infrastructure.Domain.Audits
{
    public static class BaseEntityProperties
    {
        public const string CreatedOn = nameof(CreatedOn);
        public const string ModifiedOn = nameof(ModifiedOn);


        public static void SetBaseEntityPropertiesValues(this ChangeTracker changeTracker)
        {
            var now = DateTime.Now;

            var modifiedEntries = changeTracker.Entries<BaseEntity<Guid>>()
                .Where(x => x.State == EntityState.Modified);

            foreach (var modifiedEntry in modifiedEntries)
            {
                modifiedEntry.Property(ModifiedOn).CurrentValue = now;
            }

            var createdEntries = changeTracker.Entries<BaseEntity<Guid>>()
                .Where(x => x.State == EntityState.Added);

            foreach (var createdEntry in createdEntries)
            {
                createdEntry.Property(CreatedOn).CurrentValue = now;
            }
        }
    }
}
