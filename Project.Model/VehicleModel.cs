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
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid VehicleModelId { get; set; }

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
        /// Gets or sets the Vehicle Make Identifier.
        /// </summary>
        /// <value>The Vehicle Make identifier.</value>
        public Guid VehicleMakeId { get; set; }

        /// <summary>
        /// Gets or sets the Vehicle Make Model.
        /// </summary>
        /// <value>The Vehicle Make Model.</value>
        public virtual VehicleMake VehicleMake { get; set; }
    }
}
