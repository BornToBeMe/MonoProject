using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public class Search : ISearch
    {
        /// <summary>
        /// Gets or sets the Filter.
        /// </summary>
        /// <value>Filter.</value>
        public string CurrentFilter { get; set; }
    }
}
