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
        public ModelService(IModelRepository repository)
        {
            this.Repository = repository;
        }

        protected IModelRepository Repository { get; private set; }

        public async Task<IPagedList<IVehicleModel>> SelectAllAsync(ISorting sortBy, ISearch search, IPaging pagination)
        {
            return await Repository.SelectAllAsync(sortBy, search, pagination);
        }

        public async Task<IVehicleModel> SelectByIDAsync(Guid id)
        {
            return await Repository.SelectByIDAsync(id);
        }

        public async Task<bool> CreateAsync(Project.Model.VehicleModel obj)
        {
            return await Repository.CreateAsync(obj);
        }

        public async Task<bool> EditAsync(Guid id, Project.Model.VehicleModel vehicleModel)
        {
            return await Repository.EditAsync(id, vehicleModel);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync(id);
        }

        public IList<IVehicleMake> PopulateMakesDropDownList()
        {
            return Repository.PopulateMakesDropDownList();
        }
    }
}
