using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model.Common;

namespace Project.Model
{
    public class VehicleMake : IVehicleMake
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>Name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Abrevation.
        /// </summary>
        /// <value>Abrevation.</value>
        public string Abrv { get; set; }

        /// <summary>
        /// Gets or sets the Vehicle Models.
        /// </summary>
        /// <value>Vehicle Models.</value>
        public ICollection<IVehicleModel> VehicleModels { get; set; }
    }
}
