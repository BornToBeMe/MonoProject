using Project.Common;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service
{
    public class ModelService : IModelService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IModelRepository" /> class.
        /// </summary>
        public ModelService(IModelRepository repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// Gets the Repository.
        /// </summary>
        /// <value>The Repository.</value>
        protected IModelRepository Repository { get; private set; }

        /// <summary>
        /// Gets All Vehicle Models
        /// </summary>
        /// <param name="sortOrder">Sort Order.</param>
        /// <param name="search">Search.</param>
        /// <param name="pagination">Paging.</param>
        /// <returns>Paged List of Vehicle Models</returns>
        public async Task<IPagedList<IVehicleModel>> SelectAsync(ISorting sortBy, ISearch search, IPaging pagination)
        {
            return await Repository.SelectAsync(sortBy, search, pagination);
        }

        /// <summary>
        /// Gets Vehicle Model with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        public async Task<IVehicleModel> SelectByIDAsync(Guid id)
        {
            return await Repository.SelectByIDAsync(id);
        }

        /// <summary>
        /// Creates a Vehicle Model.
        /// </summary>
        /// <param name="obj">Vehicle Model being Created.</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(IVehicleModel obj)
        {
            return await Repository.InsertAsync(obj);
        }

        /// <summary>
        /// Edits Vehicle Model with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="vehicleModel">Vehicle Model being passed in.</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid id, IVehicleModel vehicleModel)
        {
            return await Repository.UpdateAsync(id, vehicleModel);
        }

        /// <summary>
        /// Removes Vehicle Model with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync(id);
        }
    }
}
