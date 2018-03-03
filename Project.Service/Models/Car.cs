using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class Car
    {
        public int CarID { get; set; }
        public int VehicleMakeId { get; set; }
        public int VehicleModelId { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }
        public virtual VehicleModel VehicleModel { get; set; }
    }
}
