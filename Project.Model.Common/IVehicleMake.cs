using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Common
{
    public interface IVehicleMake
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        Guid Id { get; set; }

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
        /// Gets or sets the Vehicle Models.
        /// </summary>
        /// <value>Vehicle Models.</value>
        ICollection<IVehicleModel> VehicleModels { get; set; }
    }
}
