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
    public interface IMakeRepository
    {
        Task<IPagedList<IVehicleMake>> SelectAllAsync(ISorting sortOrder, ISearch search, IPaging pagination);
        Task<IVehicleMake> SelectByIDAsync(Guid id);
        Task<bool> CreateAsync(VehicleMake obj);
        Task<bool> EditAsync(Guid id, VehicleMake vehicleMake);
        Task<bool> DeleteAsync(Guid id);
    }
}
