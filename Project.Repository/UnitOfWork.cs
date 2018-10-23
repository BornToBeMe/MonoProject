using Project.Common;
using Project.DAL;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using X.PagedList;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public UnitOfWork(ICarContext carContext)
        {
            if (carContext == null)
            {
                throw new ArgumentNullException("CarContext");
            }
            CarContext = carContext;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        protected ICarContext CarContext { get; private set; }

        /// <summary>
        /// Gets all TEntity.
        /// </summary>
        /// <returns>IEnumerable<typeparamref name="TEntity"/></returns>
        public async virtual Task<IPagedList<TEntity>> SelectAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            IPaging paged = null) where TEntity : class
        {
            IQueryable<TEntity> query = CarContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToPagedListAsync(paged.PageNumber, paged.PageSize);
        }

        /// <summary>
        /// Gets single TEntity item by Id.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <returns><typeparamref name="TEntity"/></returns>
        public virtual Task<TEntity> SelectByIDAsync<TEntity>(Guid id) where TEntity : class
        {
            return CarContext.Set<TEntity>().FindAsync(id);
        }

        public Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            DbEntityEntry dbEntityEntry = CarContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                CarContext.Set<TEntity>().Add(entity);
            }
            return Task.FromResult(1);
        }

        public Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            DbEntityEntry dbEntityEntry = CarContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Deleted)
            {
                CarContext.Set<TEntity>().Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;

            return Task.FromResult(1);
        }

        public Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            DbEntityEntry dbEntityEntry = CarContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                CarContext.Set<TEntity>().Attach(entity);
                CarContext.Set<TEntity>().Remove(entity);
            }
            return Task.FromResult(1);
        }

        public Task<int> DeleteAsync<TEntity>(string Id) where TEntity : class
        {
            var entity = CarContext.Set<TEntity>().Find(Id);
            if (entity == null)
            {
                return Task.FromResult(0);
            }
            return DeleteAsync<TEntity>(entity);
        }

        public async Task<int> CommitAsync()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await CarContext.SaveChangesAsync();
                scope.Complete();
            }
            return result;
        }

        public void Dispose()
        {
            CarContext.Dispose();
        }
    }
}
