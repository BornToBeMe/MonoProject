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

        async Task<string> Sort()
        {
            using (var context = new CarContext())
            {
                var query = from c in context.VehicleMakes select c;
                switch (SortOrder)
                {
                    case "name_desc":
                        query = query.OrderByDescending(q => q.Name);
                        break;
                    case "Abrv":
                        query = query.OrderBy(q => q.Abrv);
                        break;
                    case "abrv_desc":
                        query = query.OrderByDescending(q => q.Abrv);
                        break;
                    default:
                        query = query.OrderBy(q => q.Name);
                        break;
                }
                return await Sort();
            }

        }

    }
}
