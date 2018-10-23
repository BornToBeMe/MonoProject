using AutoMapper;
using Project.Common;
using Project.DAL;
using Project.Model.Common;
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
    public class ModelRepository : IModelRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="GenericRepository{VehicleModel}" /> class.
        /// </summary>
        private readonly IGenericRepository<VehicleModel> modelRepository;

        IUnitOfWorkFactory unitOfWorkFactory { get; set; }

        public ModelRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            modelRepository = new GenericRepository<VehicleModel>(unitOfWorkFactory);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets All Vehicle Models
        /// </summary>
        /// <param name="sortOrder">Sort Order.</param>
        /// <param name="search">Search.</param>
        /// <param name="pagination">Paging.</param>
        /// <returns>Paged List of Vehicle Models</returns>
        public async Task<IPagedList<IVehicleModel>> SelectAsync(ISorting sortBy, ISearch search, IPaging pagination)
        {
            Expression<Func<VehicleModel, bool>> filter = null;
            if (!String.IsNullOrEmpty(search.CurrentFilter))
            {
                filter = q => q.Name.Contains(search.CurrentFilter) || q.Abrv.Contains(search.CurrentFilter) || q.VehicleMake.Name.Contains(search.CurrentFilter);
            }

            var query = await modelRepository.SelectAsync(
                filter: filter,
                orderBy: q =>
                {
                    if (sortBy.SortBy == "Make")
                    {
                        if (sortBy.SortAscending)
                        {
                            return q.OrderBy(i => i.VehicleMake.Name);
                        }
                        else
                        {
                            return q.OrderByDescending(i => i.VehicleMake.Name);
                        }
                    }
                    else if (sortBy.SortBy == "Name")
                    {
                        if (sortBy.SortAscending)
                        {
                            return q.OrderBy(i => i.Name);
                        }
                        else
                        {
                            return q.OrderByDescending(i => i.Name);
                        }
                    }
                    else if (sortBy.SortBy == "Abrv")
                    {
                        if (sortBy.SortAscending)
                        {
                            return q.OrderBy(i => i.Abrv);
                        }
                        else
                        {
                            return q.OrderByDescending(i => i.Abrv);
                        }
                    }
                    else
                    {
                        return q.OrderBy(i => i.Name);
                    }
                },
                includeProperties: null,
                paged: pagination
            );

            var map = Mapper.Map<IEnumerable<IVehicleModel>>(query);
            return new StaticPagedList<IVehicleModel>(map, query.GetMetaData());
        }

        /// <summary>
        /// Gets Vehicle Model with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        public async Task<IVehicleModel> SelectByIDAsync(Guid id)
        {
            IVehicleModel make = Mapper.Map<IVehicleModel>(await modelRepository.SelectByIDAsync(id));
            return make;
        }

        /// <summary>
        /// Creates a Vehicle Model.
        /// </summary>
        /// <param name="obj">Vehicle Model being Created.</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(IVehicleModel obj)
        {
            var map = Mapper.Map<VehicleModel>(obj);
            map.VehicleModelId = Guid.NewGuid();
            return await modelRepository.InsertAsync(map);
        }

        /// <summary>
        /// Edits Vehicle Model with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="vehicleModel">Vehicle Model being passed in.</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid id, IVehicleModel vehicleModel)
        {
            var updated = Mapper.Map<VehicleModel>(vehicleModel);
            return await modelRepository.UpdateAsync(updated, id);
        }

        /// <summary>
        /// Removes Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            VehicleModel existing = await modelRepository.SelectByIDAsync(id);
            return await modelRepository.DeleteAsync(existing);
        }
    }

    #endregion
}
