using Project.Common;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Common
{
    public interface IMakeService
    {
        Task<IPagedList<IVehicleMake>> SelectAllAsync(ISorting sortOrder, ISearch search, IPaging pagination);
        Task<IVehicleMake> SelectByIDAsync(Guid id);
        Task<bool> CreateAsync(IVehicleMake obj);
        Task<bool> EditAsync(Guid id, IVehicleMake vehicleMake);
        Task<bool> DeleteAsync(Guid id);
    }
}
