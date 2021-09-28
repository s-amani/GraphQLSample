using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GraphQLSample.Core.Infrastructure.Domain.BaseEntities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLSample.Core.Infrastructure.DataLayer.Repositories
{
    public interface IGenericRepository<in TKey, TEntity> where TEntity : class, IBaseEntity<TKey>
    {
        DbSet<TEntity> Set { get; }

        TEntity Add(TEntity model);
        TEntity Edit(TEntity model);

        Task<TEntity> FindAsync(TKey id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null);
        
        void Delete(TEntity model);
    }
}
