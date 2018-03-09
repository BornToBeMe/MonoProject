using Project.Service.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class Sorting : ISorting
    {
        public string SortOrder { get; set; }
    }
}
