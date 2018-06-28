using Project.Model.Common;
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

        List<IVehicleModel> GetAllVehicleModels();
    }
}
