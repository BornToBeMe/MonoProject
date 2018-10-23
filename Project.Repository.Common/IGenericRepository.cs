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
        Task<IPagedList<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties,
            IPaging paged);
        Task<TEntity> SelectByIDAsync(Guid id);
        Task<bool> InsertAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity, Guid id);
    }
}
