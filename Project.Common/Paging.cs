using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public class Paging : IPaging
    {
        /// <summary>
        /// Gets or sets the Page Number.
        /// </summary>
        /// <value>Page Number.</value>
        public int? PageNumber { get; set; } = 1;

        /// <summary>
        /// Gets or sets the Page Size.
        /// </summary>
        /// <value>Page Size.</value>
        public int? PageSize { get; set; } = 3;
    }
}
