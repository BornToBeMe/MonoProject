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
        Task<IPagedList<VehicleMake>> SelectAllAsync(Sorting sortOrder, string currentFilter, string searchString, Paging pagination);
        Task<VehicleMake> SelectByIDAsync(Guid id);
        Task<bool> InsertAsync(VehicleMake obj);
        Task<VehicleMake> UpdateAsync(Guid id, VehicleMake vehicleMake);
        Task<bool> DeleteAsync(Guid id);
    }
}
