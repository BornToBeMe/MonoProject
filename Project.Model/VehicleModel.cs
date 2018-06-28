using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class VehicleModel : IVehicleModel
    {
        public Guid VehicleModelId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public Guid VehicleMakeId { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }
    }
}
