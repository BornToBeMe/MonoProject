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
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets All TEntity
        /// </summary>
        /// <param name="sortOrder">Sort Order.</param>
        /// <param name="search">Search.</param>
        /// <param name="pagination">Paging.</param>
        /// <returns>Paged List of Vehicle Models</returns>
        Task<IPagedList<TEntity>> SelectAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties,
            IPaging paged) where TEntity : class;

        /// <summary>
        /// Gets TEntity with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        Task<TEntity> SelectByIDAsync<TEntity>(Guid id) where TEntity : class;

        /// <summary>
        /// Adds TEntity item into database.
        /// </summary>
        /// <param name="entity">Item to be added.</param>
        /// <returns></returns>
        Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Saves changes to database
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();

        /// <summary>
        /// Removes TEntity item from database.
        /// </summary>
        /// <param name="entity">Item to be deleted.</param>
        /// <returns></returns>
        Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Removes TEntity item from database.
        /// </summary>
        /// <param name="Id">Item to be deleted.</param>
        /// <returns></returns>
        Task<int> DeleteAsync<TEntity>(string Id) where TEntity : class;

        /// <summary>
        /// Updates existing item with new
        /// </summary>
        /// <param name="entity">Item to be updated.</param>
        /// <param name="id">The item identifier.</param>
        /// <returns></returns>
        Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
    }
}
