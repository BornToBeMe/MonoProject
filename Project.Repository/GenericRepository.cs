using AutoMapper;
using Project.Common;
using Project.DAL;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        IUnitOfWorkFactory uowFactory;

        public GenericRepository(IUnitOfWorkFactory uowFactory)
        {
            this.uowFactory = uowFactory;
        }

        /// <summary>
        /// Gets all TEntity.
        /// </summary>
        /// <returns>IEnumerable<typeparamref name="TEntity"/></returns>
        public async virtual Task<IPagedList<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties,
            IPaging paged)
        {
            var unitOfWork = uowFactory.CreateUnitOfWork();
            return await unitOfWork.SelectAsync(filter, orderBy, includeProperties, paged);
        }

        /// <summary>
        /// Gets single TEntity item by Id.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <returns><typeparamref name="TEntity"/></returns>
        public async virtual Task<TEntity> SelectByIDAsync(Guid id)
        {
            var unitOfWork = uowFactory.CreateUnitOfWork();
            return await unitOfWork.SelectByIDAsync<TEntity>(id);
        }

        /// <summary>
        /// Adds TEntity item into database.
        /// </summary>
        /// <param name="entity">Item to be added.</param>
        /// <returns></returns>
        public async virtual Task<bool> InsertAsync(TEntity entity)
        {
            var unitOfWork = uowFactory.CreateUnitOfWork();
            await unitOfWork.InsertAsync(entity);
            return (await unitOfWork.CommitAsync()) > 0;
        }

        /// <summary>
        /// Removes TEntity item from database.
        /// </summary>
        /// <param name="entity">Item to be deleted.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(TEntity entity)
        {
            var unitOfWork = uowFactory.CreateUnitOfWork();
            await unitOfWork.DeleteAsync(entity);
            return (await unitOfWork.CommitAsync()) > 0;
        }

        /// <summary>
        /// Updates existing item with new
        /// </summary>
        /// <param name="entity">Item to be updated.</param>
        /// <param name="id">The item identifier.</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(TEntity entity, Guid id)
        {
            var unitOfWork = uowFactory.CreateUnitOfWork();
            await unitOfWork.UpdateAsync(entity);
            return (await unitOfWork.CommitAsync()) > 0;
        }
    }
}