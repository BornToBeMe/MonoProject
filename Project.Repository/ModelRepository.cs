using AutoMapper;
using Project.Common;
using Project.DAL;
using Project.Model.Common;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Repository
{
    public class ModelRepository : IModelRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        private UnitOfWork unitOfWork = new UnitOfWork();

        #endregion

        #region Methods

        /// <summary>
        /// Gets All Vehicle Models
        /// </summary>
        /// <param name="sortOrder">Sort Order.</param>
        /// <param name="search">Search.</param>
        /// <param name="pagination">Paging.</param>
        /// <returns>Paged List of Vehicle Models</returns>
        public async Task<IPagedList<IVehicleModel>> SelectAllAsync(ISorting sortBy, ISearch search, IPaging pagination)
        {
            var query = await unitOfWork.ModelRepository.GetAllAsync();

            if (!String.IsNullOrEmpty(search.CurrentFilter))
            {
                query = query.Where(q => q.Name.Contains(search.CurrentFilter) || q.Abrv.Contains(search.CurrentFilter) || q.VehicleMake.Name.Contains(search.CurrentFilter));
            }

            if (sortBy.SortBy == "Make")
            {
                if (sortBy.SortAscending)
                {
                    query = query.OrderBy(q => q.VehicleMake.Name);
                }
                else
                {
                    query = query.OrderByDescending(q => q.VehicleMake.Name);
                }
            }
            else if (sortBy.SortBy == "Name")
            {
                if (sortBy.SortAscending)
                {
                    query = query.OrderBy(q => q.Name);
                }
                else
                {
                    query = query.OrderByDescending(q => q.Name);
                }
            }
            else if (sortBy.SortBy == "Abrv")
            {
                if (sortBy.SortAscending)
                {
                    query = query.OrderBy(q => q.Abrv);
                }
                else
                {
                    query = query.OrderByDescending(q => q.Abrv);
                }
            }
            else
            {
                query = query.OrderBy(q => q.Name);
            }

            int pageSize = (pagination.PageSize ?? 3);
            int pageNumber = (pagination.PageNumber ?? 1);

            var map = Mapper.Map<IEnumerable<IVehicleModel>>(await query.ToListAsync());
            return map.ToPagedList(pageNumber, pageSize);

        }

        /// <summary>
        /// Gets Vehicle Model with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        public async Task<IVehicleModel> SelectByIDAsync(Guid id)
        {
            IVehicleModel make = Mapper.Map<IVehicleModel>(await unitOfWork.ModelRepository.GetByID(id));
            return make;
        }

        /// <summary>
        /// Creates a Vehicle Model.
        /// </summary>
        /// <param name="obj">Vehicle Model being Created.</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(IVehicleModel obj)
        {
            var map = Mapper.Map<VehicleModel>(obj);
            map.VehicleModelId = Guid.NewGuid();
            await unitOfWork.ModelRepository.Insert(map);
            return (await unitOfWork.Save() > 0);
        }

        /// <summary>
        /// Edits Vehicle Model with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="vehicleModel">Vehicle Model being passed in.</param>
        /// <returns></returns>
        public async Task<bool> EditAsync(Guid id, IVehicleModel vehicleModel)
        {
            var entity = await unitOfWork.ModelRepository.GetByID(id);
            var map = Mapper.Map<Project.Model.VehicleModel>(entity);
            vehicleModel.VehicleModelId = map.VehicleModelId;
            if (map == null)
            {
                throw new ArgumentNullException();
            }
            // Context.Entry(entity).CurrentValues.SetValues(vehicleModel);

            return (await unitOfWork.Save() > 0);
        }

        /// <summary>
        /// Removes Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            VehicleModel existing = await unitOfWork.ModelRepository.GetByID(id);
            await unitOfWork.ModelRepository.DeleteAsync(existing);
            return (await unitOfWork.Save() > 0);
        }

        /// <summary>
        /// Gets a List of Vehicle Makes.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<IVehicleMake>> PopulateMakesDropDownList()
        {
            List<IVehicleMake> makes = Mapper.Map<IEnumerable<VehicleMake>, List<IVehicleMake>>(await unitOfWork.MakeRepository.GetAllAsync());
            return makes;
        }
    }

    #endregion
}
