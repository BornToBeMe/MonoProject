using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Services
{
    public interface IVehicleModelService
    {
        Task<IPagedList<VehicleModel>> SelectAllAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task<VehicleModel> SelectByIDAsync(Guid id);
        Task<string> InsertAsync(VehicleModel obj);
        Task<VehicleModel> UpdateAsync(Guid id, VehicleModel vehicleModel);
        Task<string> DeleteAsync(Guid id);
    }
}
