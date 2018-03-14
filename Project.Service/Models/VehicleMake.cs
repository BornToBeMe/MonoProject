using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class VehicleMake
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Abrv { get; set; }

        public virtual ICollection<VehicleModel> VehicleModel { get; set; }
    }
}
