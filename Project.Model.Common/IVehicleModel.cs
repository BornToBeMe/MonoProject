using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Common
{
    public interface IVehicleModel
    {
        Guid VehicleModelId { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        Guid VehicleMakeId { get; set; }
    }
}
