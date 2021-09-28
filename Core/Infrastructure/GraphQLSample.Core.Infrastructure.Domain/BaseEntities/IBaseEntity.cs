using System;

namespace GraphQLSample.Core.Infrastructure.Domain.BaseEntities
{
    /// <summary>
    /// Represents the base Entity
    /// </summary>
    public interface IBaseEntity<T> : IEntity<T>
    {
        #region Properties

        /// <summary>
        /// gets or sets date that this entity was created
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// gets or sets Date that this entity was updated
        /// </summary>
        DateTime ModifiedAt { get; set; }

        #endregion

    }
}
