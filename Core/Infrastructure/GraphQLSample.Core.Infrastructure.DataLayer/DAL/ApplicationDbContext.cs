using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using GraphQLSample.Core.Infrastructure.Domain.Audits;
using GraphQLSample.Core.Infrastructure.Domain.Config;
using GraphQLSample.Shared.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace GraphQLSample.Core.Infrastructure.DataLayer.DAL
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(CustomerConfig)));
        }

        #region BaseClass


        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
        {
            var value = Entry(entity).Property(propertyName).CurrentValue;
            return value != null
                ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
                : default(T);
        }

        public object GetShadowPropertyValue(object entity, string propertyName)
        {
            return Entry(entity).Property(propertyName).CurrentValue;
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Update(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();

            BeforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false;

            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            
            ChangeTracker.AutoDetectChangesEnabled = true;
            
            return result;
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            BeforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false;

            var result = base.SaveChanges();
            
            ChangeTracker.AutoDetectChangesEnabled = true;
            
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            BeforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false;

            var result = base.SaveChangesAsync(cancellationToken);
            
            ChangeTracker.AutoDetectChangesEnabled = true;
            
            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            BeforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false;

            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            
            ChangeTracker.AutoDetectChangesEnabled = true;
            
            return result;
        }

        private void BeforeSaveTriggers()
        {
            ValidateEntities();

            SetAuditProperties();
        }

        private void SetAuditProperties()
        {
            ChangeTracker.SetBaseEntityPropertiesValues();
        }

        private void ValidateEntities()
        {
            var errors = this.GetValidationErrors();

            if (string.IsNullOrWhiteSpace(errors)) 
                return;

            var loggerFactory = this.GetService<ILoggerFactory>();
            
            loggerFactory.ThrowExceptionIfNull(nameof(loggerFactory));

            var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
            logger.LogError(errors);

            throw new InvalidOperationException(errors);
        }

        #endregion
    }

}
