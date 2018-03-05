using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Services
{
    public interface IVehicleMakeService
    {
        Task<IPagedList<VehicleMake>> SelectAllAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task<VehicleMake> SelectByIDAsync(Guid id);
        Task<string> InsertAsync(VehicleMake obj);
        Task<VehicleMake> UpdateAsync(Guid id, VehicleMake vehicleMake);
        Task<string> DeleteAsync(Guid id);
    }
}
