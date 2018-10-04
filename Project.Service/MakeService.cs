using Project.Common;
using Project.Model;
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
    public class MakeService : IMakeService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IMakeRepository" /> class.
        /// </summary>
        public MakeService(IMakeRepository repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// Gets the Repository.
        /// </summary>
        /// <value>The Repository.</value>
        protected IMakeRepository Repository { get; private set; }

        /// <summary>
        /// Gets All Vehicle Makes
        /// </summary>
        /// <param name="sortOrder">Sort Order.</param>
        /// <param name="search">Search.</param>
        /// <param name="pagination">Paging.</param>
        /// <returns>Paged List of Vehicle Makes</returns>
        public async Task<IPagedList<IVehicleMake>> SelectAllAsync(ISorting sortBy, ISearch search, IPaging pagination)
        {
            return await Repository.SelectAllAsync(sortBy, search, pagination);
        }

        /// <summary>
        /// Gets Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        public async Task<IVehicleMake> SelectByIDAsync(Guid id)
        {
            return await Repository.SelectByIDAsync(id);
        }

        /// <summary>
        /// Creates a Vehicle Make.
        /// </summary>
        /// <param name="obj">Vehicle Make being Created.</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(IVehicleMake obj)
        {
            return await Repository.CreateAsync(obj);
        }

        /// <summary>
        /// Edits Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="vehicleMake">Vehicle Make being passed in.</param>
        /// <returns></returns>
        public async Task<bool> EditAsync(Guid id, IVehicleMake vehicleMake)
        {
            return await Repository.EditAsync(id, vehicleMake);
        }

        /// <summary>
        /// Removes Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync(id);
        }

    }
}
