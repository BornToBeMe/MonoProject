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
    public interface IModelService
    {
        /// <summary>
        /// Gets All Vehicle Models
        /// </summary>
        /// <param name="sortOrder">Sort Order.</param>
        /// <param name="search">Search.</param>
        /// <param name="pagination">Paging.</param>
        /// <returns>Paged List of Vehicle Models</returns>
        Task<IPagedList<IVehicleModel>> SelectAllAsync(ISorting sortOrder, ISearch search, IPaging pagination);

        /// <summary>
        /// Gets Vehicle Model with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        Task<IVehicleModel> SelectByIDAsync(Guid id);

        /// <summary>
        /// Creates a Vehicle Model.
        /// </summary>
        /// <param name="obj">Vehicle Model being Created.</param>
        /// <returns></returns>
        Task<bool> CreateAsync(IVehicleModel obj);

        /// <summary>
        /// Edits Vehicle Model with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="vehicleModel">Vehicle Model being passed in.</param>
        /// <returns></returns>
        Task<bool> EditAsync(Guid id, IVehicleModel vehicleModel);

        /// <summary>
        /// Removes Vehicle Model with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Gets a List of Vehicle Makes.
        /// </summary>
        /// <returns></returns>
        Task<IList<IVehicleMake>> PopulateMakesDropDownList();
    }
}
