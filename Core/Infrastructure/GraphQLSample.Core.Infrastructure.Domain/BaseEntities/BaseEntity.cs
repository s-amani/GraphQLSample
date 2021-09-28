using System;

namespace GraphQLSample.Core.Infrastructure.Domain.BaseEntities
{
    /// <summary>
    /// Represents the base Entity
    /// </summary>
    public abstract class BaseEntity<T> : Entity<T>, IBaseEntity<T>
    {
        #region Properties

        /// <summary>
        /// gets or sets date that this entity was created
        /// </summary>
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// gets or sets Date that this entity was updated
        /// </summary>
        public virtual DateTime ModifiedAt { get; set; }

        #endregion

    }
}
