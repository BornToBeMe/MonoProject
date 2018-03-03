using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class Car
    {
        public Guid CarID { get; set; }
        public Guid VehicleMakeId { get; set; }
        public Guid VehicleModelId { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }
        public virtual VehicleModel VehicleModel { get; set; }
    }
}
