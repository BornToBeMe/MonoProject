using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class Filtering : IPaging, ISorting, ISearch
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public string SortOrder { get; set; }

        public string CurrentFilter { get; set; }
    }
}
