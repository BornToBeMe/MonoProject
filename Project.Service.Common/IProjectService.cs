using Project.Model.Common;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IProjectService
    {
        List<IVehicleMake> GetAllVehicleMakes();
        IVehicleMake GetVehicleMake(Guid id);
        bool PutVehicleMake(Guid id, VehicleMake vehicleMake);
        bool PostVehicleMake(VehicleMake obj);
        bool DeleteVehicleMake(Guid id);

        List<IVehicleModel> GetAllVehicleModels();
        IVehicleModel GetVehicleModel(Guid id);
        IVehicleModel PutVehicleModel(Guid id, IVehicleModel vehicleModel);
        bool PostVehicleModel(IVehicleModel obj);
        bool DeleteVehicleModel(Guid id);
    }
}
