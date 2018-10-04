using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public interface IPaging
    {
        /// <summary>
        /// Gets or sets the Page Number.
        /// </summary>
        /// <value>Page Number.</value>
        int? PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the Page Size.
        /// </summary>
        /// <value>Page Size.</value>
        int? PageSize { get; set; }
    }
}
