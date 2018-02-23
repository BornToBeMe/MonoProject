using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class VehicleModel
    {
        public int VehicleModelID { get; set; }
        public int VehicleMakeID { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
