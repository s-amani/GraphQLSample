using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GraphQLSample.Core.Infrastructure.DataLayer.DAL;
using GraphQLSample.Core.Infrastructure.Domain.BaseEntities;
using GraphQLSample.Shared.Common;
using Microsoft.EntityFrameworkCore;

namespace GraphQLSample.Core.Infrastructure.DataLayer.Repositories
{
    public class GenericRepository<TKey, TEntity> : IGenericRepository<TKey, TEntity> where TEntity : class, IBaseEntity<TKey>
    {
        #region Fields

        public DbSet<TEntity> Set { get; }

        #endregion

        #region Ctor

        public GenericRepository(IUnitOfWork uow)
        {
            uow.ThrowExceptionIfNull(nameof(uow));
            Set = uow.Set<TEntity>();
        }

        #endregion



        public TEntity Add(TEntity model)
        {
            Set.Add(model);

            return model;
        }

        public TEntity Edit(TEntity model)
        {
            Set.Update(model);

            return model;
        }

        public async Task<TEntity> FindAsync(TKey id)
        {
            return await Set.FindAsync(id);
        }

        public async Task<TEntity>  FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Set.FindAsync(predicate);
        }

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = Set.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }

        public void Delete(TEntity model)
        {
            Set.Remove(model);
        }
    }
}
