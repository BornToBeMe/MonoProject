using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IProjectRepository
    {

        List<IVehicleMake> GetAllVehicleMakes();
        IVehicleMake GetVehicleMake(Guid id);
        IVehicleMake PutVehicleMake(Guid id, IVehicleMake vehicleMake);
        bool PostVehicleMake(IVehicleMake obj);
        bool DeleteVehicleMake(Guid id);

        List<IVehicleModel> GetAllVehicleModels();
        IVehicleModel GetVehicleModel(Guid id);
        IVehicleModel PutVehicleModel(Guid id, IVehicleModel vehicleModel);
        bool PostVehicleModel(IVehicleModel obj);
        bool DeleteVehicleModel(Guid id);
    }
}
