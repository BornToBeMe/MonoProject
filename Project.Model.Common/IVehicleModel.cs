using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Common
{
    public interface IVehicleModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        Guid VehicleModelId { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>Name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the Abrevation.
        /// </summary>
        /// <value>Abrevation.</value>
        string Abrv { get; set; }

        /// <summary>
        /// Gets or sets the Vehicle Make Identifier.
        /// </summary>
        /// <value>The Vehicle Make identifier.</value>
        Guid VehicleMakeId { get; set; }
    }
}
