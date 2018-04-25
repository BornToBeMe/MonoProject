using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class Sorting : ISorting
    {
        public string SortBy { get; set; }
        public bool SortAscending { get; set; }
    }
}
