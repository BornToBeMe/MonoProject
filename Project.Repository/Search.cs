using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class Search : ISearch
    {
        public string CurrentFilter { get; set; }
    }
}
