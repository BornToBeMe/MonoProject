using AutoMapper;
using Project.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.DAL
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        internal ICarContext context;
        internal DbSet<TEntity> dbSet;

        /// <summary>
        /// Initializes a new instance of the GenericRepository class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GenericRepository(ICarContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Gets all TEntity.
        /// </summary>
        /// <returns>IEnumerable<typeparamref name="TEntity"/></returns>
        public async virtual Task<IPagedList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            IPaging paged = null)
        {
            IQueryable<TEntity> query = dbSet;

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

        public async virtual Task<IEnumerable<TEntity>> GetListAsync()
        {
            return await dbSet.ToListAsync();
        }

        /// <summary>
        /// Gets single TEntity item by Id.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <returns><typeparamref name="TEntity"/></returns>
        public virtual Task<TEntity> GetByID(object id)
        {
            return dbSet.FindAsync(id);
        }

        /// <summary>
        /// Adds TEntity item into database.
        /// </summary>
        /// <param name="entity">Item to be added.</param>
        /// <returns></returns>
        public async virtual Task<int> Insert(TEntity entity)
        {
            dbSet.Add(entity);
            return await context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes TEntity item from database.
        /// </summary>
        /// <param name="entity">Item to be deleted.</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            return await context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates existing item with new
        /// </summary>
        /// <param name="entity">Item to be updated.</param>
        /// <param name="id">The item identifier.</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(TEntity entity, Guid id)
        {
            var item = await dbSet.FindAsync(id);
            context.Entry<TEntity>(item).CurrentValues.SetValues(entity);
            return await context.SaveChangesAsync();
        }
    }
}