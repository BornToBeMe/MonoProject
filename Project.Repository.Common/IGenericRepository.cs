using Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Repository.Common
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all TEntity.
        /// </summary>
        /// <returns>IEnumerable<typeparamref name="TEntity"/></returns>
        Task<IPagedList<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties,
            IPaging paged);

        /// <summary>
        /// Gets single TEntity item by Id.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <returns><typeparamref name="TEntity"/></returns>
        Task<TEntity> SelectByIDAsync(Guid id);

        /// <summary>
        /// Adds TEntity item into database.
        /// </summary>
        /// <param name="entity">Item to be added.</param>
        /// <returns></returns>
        Task<bool> InsertAsync(TEntity entity);

        /// <summary>
        /// Removes TEntity item from database.
        /// </summary>
        /// <param name="entity">Item to be deleted.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TEntity entity);

        /// <summary>
        /// Updates existing item with new
        /// </summary>
        /// <param name="entity">Item to be updated.</param>
        /// <param name="id">The item identifier.</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, Guid id);
    }
}
