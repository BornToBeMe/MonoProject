using Project.Model;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Repository.Common
{
    public interface IModelRepository
    {
        Task<IPagedList<IVehicleModel>> SelectAllAsync(ISorting sortOrder, ISearch search, IPaging pagination);
        Task<IVehicleModel> SelectByIDAsync(Guid id);
        Task<bool> CreateAsync(VehicleModel obj);
        Task<bool> EditAsync(Guid id, VehicleModel vehicleModel);
        Task<bool> DeleteAsync(Guid id);
        IList<VehicleMake> PopulateMakesDropDownList();
    }
}
