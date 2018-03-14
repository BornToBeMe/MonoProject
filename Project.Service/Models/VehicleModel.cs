using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class VehicleModel
    {
        public Guid VehicleModelId { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Abrv { get; set; }

        public Guid VehicleMakeId { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }

    }
}
