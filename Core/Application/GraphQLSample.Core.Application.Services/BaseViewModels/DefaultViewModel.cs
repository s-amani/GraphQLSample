using System;

namespace GraphQLSample.Core.Application.Services.BaseViewModels
{
    public class DefaultViewModel<TKeyType> : BaseIdViewModel<TKeyType>
    {
        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Last modified date
        /// </summary>
        public DateTime ModifiedAt { get; set; }
    }
}
