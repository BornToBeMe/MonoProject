using Project.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        public async virtual Task<IEnumerable<TEntity>> GetAll() {
            return await dbSet.ToListAsync();
        }

        /// <summary>
        /// Gets single TEntity item by Id.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <returns><typeparamref name="TEntity"/></returns>
        public async virtual Task<TEntity> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
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
            context.Entry<TEntity>(dbSet.Find(id)).CurrentValues.SetValues(entity);
            return await context.SaveChangesAsync();
        }
    }
}