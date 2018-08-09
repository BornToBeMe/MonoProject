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
        public MakeService(IMakeRepository repository)
        {
            this.Repository = repository;
        }

        protected IMakeRepository Repository { get; private set; }

        public async Task<IPagedList<IVehicleMake>> SelectAllAsync(ISorting sortBy, ISearch search, IPaging pagination)
        {
            return await Repository.SelectAllAsync(sortBy, search, pagination);
        }

        public async Task<IVehicleMake> SelectByIDAsync(Guid id)
        {
            return await Repository.SelectByIDAsync(id);
        }

        public async Task<bool> CreateAsync(Project.Model.VehicleMake obj)
        {
            return await Repository.CreateAsync(obj);
        }

        public async Task<bool> EditAsync(Guid id, Project.Model.VehicleMake vehicleMake)
        {
            return await Repository.EditAsync(id, vehicleMake);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync(id);
        }

    }
}
