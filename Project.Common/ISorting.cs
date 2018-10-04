using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public interface ISorting
    {
        /// <summary>
        /// Gets or sets Sorting by item.
        /// </summary>
        /// <value>Sorting.</value>
        string SortBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is in stock.
        /// </summary>
        /// <value><c>true</c> if Ascending; otherwise, <c>false</c>.</value>
        bool SortAscending { get; set; }
    }
}
