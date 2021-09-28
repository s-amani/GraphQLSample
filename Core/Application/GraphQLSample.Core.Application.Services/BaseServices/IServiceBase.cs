using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GraphQLSample.Core.Application.Services.GraphTypes;
using GraphQLSample.Core.Infrastructure.DataLayer.Repositories;
using GraphQLSample.Core.Infrastructure.Domain.BaseEntities;

namespace GraphQLSample.Core.Application.Services.BaseServices
{
    public interface IServiceBase<in TKey, TEntity, TListItemViewModel, 
        in TAddViewModel, TEditViewModel> where TEntity : class, IBaseEntity<TKey>
    {
        #region Set

        IGenericRepository<TKey, TEntity> Repository { get; }
        
        #endregion

        #region GetAsync

        Task<IReadOnlyList<TListItemViewModel>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>>[] includedProperties = null);

        #endregion

        #region GetOneAsync

        Task<TListItemViewModel> GetOneAsync(TKey id);
        Task<TListItemViewModel> GetOneAsync(Expression<Func<TEntity, bool>> filter);

        #endregion

        #region IsExists

        Task<bool> IsExists(Expression<Func<TEntity, bool>> filter);

        #endregion

        #region GetEditable

        Task<TEditViewModel> GetEditableAsync(TKey id);
        Task<TEditViewModel> GetEditableIncludeAsync(TKey id);
        Task<TEditViewModel> GetEditableIncludeAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEditViewModel> GetEditableAsync(Expression<Func<TEntity, bool>> filter);

        #endregion

        #region Add Methods

        Task<GraphQLPayload<TEntity>> AddAsync(TAddViewModel viewModel);
        Task<TEntity> AddEntityAsync(TEntity dbModel);
        TEntity AddEntity(TEntity dbModel);
        void AddRange(IEnumerable<TEntity> entities);

        #endregion

        #region Edit Methods

        Task<GraphQLPayload<TEntity>> EditAsync(TEditViewModel viewModel);
        Task<TEntity> EditEntityAsync(TEntity entity);

        #endregion

        #region DeleteMethods

        Task<bool> DeleteAsync(TKey id);

        #endregion

    }
}
 