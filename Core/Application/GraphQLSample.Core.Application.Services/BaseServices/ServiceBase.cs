using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using GraphQLSample.Core.Application.Services.BaseViewModels;
using GraphQLSample.Core.Application.Services.GraphTypes;
using GraphQLSample.Core.Infrastructure.DataLayer.DAL;
using GraphQLSample.Core.Infrastructure.DataLayer.Repositories;
using GraphQLSample.Core.Infrastructure.Domain.BaseEntities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLSample.Core.Application.Services.BaseServices
{
    public abstract class ServiceBase<TKey, TEntity, TListItemViewModel, TAddViewModel, TEditViewModel> :
          IServiceBase<TKey, TEntity, TListItemViewModel, TAddViewModel, TEditViewModel>
          where TEntity : class, IBaseEntity<TKey>
          where TEditViewModel : BaseIdViewModel<TKey>
          where TListItemViewModel : DefaultViewModel<TKey>
          where TAddViewModel : new()
    {

        #region Fields

        protected IUnitOfWork UnitOfWork { get; }

        #endregion

        #region Ctor

        protected ServiceBase(IUnitOfWork unitOfWork, IGenericRepository<TKey, TEntity> repository, IMapper mapper)
        {
            Mapper = mapper;
         
            UnitOfWork = unitOfWork;
            Repository = repository;
        }

        #endregion

        #region Properties

        protected IMapper Mapper { get; }

        public IGenericRepository<TKey, TEntity> Repository { get; }

        #endregion

        #region Filter

        public abstract IQueryable<TEntity> FilterByTerm(IQueryable<TEntity> query, string title);

        #endregion

        #region Custom Mappings

        public virtual async Task<List<TListItemViewModel>> CustomListMapping(List<TListItemViewModel> list)
        {
            foreach (var item in list)
            {
                await CustomModelMapping(item);
            }
            return list;
        }

        public virtual Task CustomModelMapping(TListItemViewModel model)
        {
            return Task.FromResult(model);
        }

        #endregion

        #region Triggers

        public virtual Task OnBeforeDeleteAsync(TEntity model)
        {
            return Task.FromResult(model);
        }

        public virtual Task OnBeforeAddMapping(TAddViewModel model)
        {
            return Task.FromResult(model);
        }

        public virtual Task OnBeforeEditMapping(TEditViewModel model)
        {
            return Task.FromResult(model);
        }

        public virtual Task OnBeforeAddSaveChanges(TEntity dbModel, TAddViewModel model)
        {
            return Task.FromResult(false);
        }

        public virtual Task OnBeforeEditSaveChanges(TEntity dbModel, TEditViewModel model)
        {
            return Task.FromResult(false);
        }

        #endregion

        #region IncludeOnLoading

        public virtual IQueryable<TEntity> IncludeOnLoading(IQueryable<TEntity> query)
        {
            return query;
        }

        #endregion

        #region Get Methods

        public virtual async Task<IReadOnlyList<TListItemViewModel>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>>[] includedProperties = null)
        {
            var query = Repository.FindAll();

            query = IncludeOnLoading(query);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includedProperties != null)
            {
                query = includedProperties.Aggregate(query, (current, exp) => current.Include(exp));
            }

            var temp = await query.ToListAsync();
            var list = Mapper.Map<List<TListItemViewModel>>(temp);

            return list;
        }

        public virtual async Task<TListItemViewModel> GetOneAsync(TKey id)
        {
            var query = Repository.FindAll().AsNoTracking();
            query = IncludeOnLoading(query);
            var dbModel = await query.SingleOrDefaultAsync(x => x.Id.Equals(id));
            var viewModel = Mapper.Map<TListItemViewModel>(dbModel);

            await CustomModelMapping(viewModel);

            return viewModel;
        }

        public virtual async Task<TListItemViewModel> GetOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            var query = Repository.FindAll().AsNoTracking();
            query = IncludeOnLoading(query);
            var model = await query.FirstOrDefaultAsync(filter);
            var viewModel = Mapper.Map<TListItemViewModel>(model);

            await CustomModelMapping(viewModel);

            return viewModel;
        }

        #endregion

        #region GetEditableAsync

        public virtual async Task<TEditViewModel> GetEditableAsync(TKey id)
        {
            var query = Repository.FindAll(x=> Equals(x.Id, id));
            query = IncludeOnLoading(query);
            var dbModel = await query.SingleOrDefaultAsync(x => x.Id.Equals(id));
            var viewModel = Mapper.Map<TEditViewModel>(dbModel);

            return viewModel;

        }

        public virtual async Task<TEditViewModel> GetEditableIncludeAsync(TKey id)
        {
            var result = await GetEditableIncludeAsync(x => x.Id.Equals(id));
            return result;
        }

        public virtual async Task<TEditViewModel> GetEditableIncludeAsync(Expression<Func<TEntity, bool>> filter)
        {
            var query = Repository.FindAll(filter);
            query = IncludeOnLoading(query);
            var dbModel = await query.SingleOrDefaultAsync(filter);
            var viewModel = Mapper.Map<TEditViewModel>(dbModel);
            return viewModel;
        }

        public virtual async Task<TEditViewModel> GetEditableAsync(Expression<Func<TEntity, bool>> filter)
        {
            var model = await Repository.FindAsync(filter);
            var viewModel = Mapper.Map<TEditViewModel>(model);

            return viewModel;
        }

        #endregion

        #region IsExists

        public virtual async Task<bool> IsExists(Expression<Func<TEntity, bool>> filter)
        {
            return await Repository.FindAll().AnyAsync(filter);
        }

        #endregion

        #region Add Methods

        public virtual async Task<GraphQLPayload<TEntity>> AddAsync(TAddViewModel viewModel)
        {
            await OnBeforeAddMapping(viewModel);

            var dbModel = Mapper.Map<TEntity>(viewModel);

            Repository.Add(dbModel);

            await OnBeforeAddSaveChanges(dbModel, viewModel);

            await UnitOfWork.SaveChangesAsync();

            return new GraphQLPayload<TEntity>(dbModel);
        }

        public async Task<TEntity> AddEntityAsync(TEntity dbModel)
        {
            Repository.Add(dbModel);

            await UnitOfWork.SaveChangesAsync();

            return dbModel;
        }

        public TEntity AddEntity(TEntity dbModel)
        {
            Repository.Add(dbModel);

            UnitOfWork.SaveChanges();

            return dbModel;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Repository.Set.AddRange(entities);
            UnitOfWork.SaveChanges();
        }

        #endregion

        #region EditRecord

        public virtual async Task<GraphQLPayload<TEntity>> EditAsync(TEditViewModel viewModel)
        {
            var dbModel = await Repository.FindAsync(viewModel.Id);

            if (dbModel == null)
                return null;

            await OnBeforeEditMapping(viewModel);

            Mapper.Map(viewModel, dbModel);

            await OnBeforeEditSaveChanges(dbModel, viewModel);

            await UnitOfWork.SaveChangesAsync();

            return new GraphQLPayload<TEntity>(dbModel);
        }

        public async Task<TEntity> EditEntityAsync(TEntity entity)
        {
            UnitOfWork.Entry(entity).State = EntityState.Modified;
            var result = await UnitOfWork.SaveChangesAsync() > 0;

            return entity;
        }

        #endregion

        #region DeleteRecord

        public virtual async Task<bool> DeleteAsync(TKey id)
        {
            var dbModel = await Repository.FindAsync(id);

            if (dbModel == null)
                return false;

            Repository.Delete(dbModel);

            await OnBeforeDeleteAsync(dbModel);

           var result = await UnitOfWork.SaveChangesAsync() > 0;

           return result;

        }


        #endregion

    }
}
