using Project.Common;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Common
{
    public interface IMakeService
    {
        /// <summary>
        /// Gets All Vehicle Makes
        /// </summary>
        /// <param name="sortOrder">Sort Order.</param>
        /// <param name="search">Search.</param>
        /// <param name="pagination">Paging.</param>
        /// <returns>Paged List of Vehicle Makes</returns>
        Task<IPagedList<IVehicleMake>> SelectAsync(ISorting sortOrder, ISearch search, IPaging pagination);

        /// <summary>
        /// Gets Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        Task<IVehicleMake> SelectByIDAsync(Guid id);

        /// <summary>
        /// Creates a Vehicle Make.
        /// </summary>
        /// <param name="obj">Vehicle Make being Created.</param>
        /// <returns></returns>
        Task<bool> InsertAsync(IVehicleMake obj);

        /// <summary>
        /// Edits Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="vehicleMake">Vehicle Make being passed in.</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Guid id, IVehicleMake vehicleMake);

        /// <summary>
        /// Removes Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
