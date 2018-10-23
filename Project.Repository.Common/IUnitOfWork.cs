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
        Task<IPagedList<TEntity>> SelectAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties,
            IPaging paged) where TEntity : class;
        Task<TEntity> SelectByIDAsync<TEntity>(Guid id) where TEntity : class;
        Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<int> CommitAsync();
        Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<int> DeleteAsync<TEntity>(string ID) where TEntity : class;
        Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
    }
}
