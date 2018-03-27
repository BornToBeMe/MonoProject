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
        Task<IPagedList<VehicleModel>> SelectAllAsync(ISorting sorting, ISearch search, IPaging pagination);
        Task<VehicleModel> SelectByIDAsync(Guid id);
        Task<bool> InsertAsync(VehicleModel obj);
        Task<VehicleModel> UpdateAsync(Guid id, VehicleModel vehicleModel);
        Task<bool> DeleteAsync(Guid id);
        IList<VehicleMake> PopulateMakesDropDownList();
    }
}
