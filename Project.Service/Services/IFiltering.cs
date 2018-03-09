using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public interface IPaging
    {
        int? PageNumber { get; set; }
        int PageSize { get; set; }
    }

    public interface ISorting
    {
        string SortOrder { get; set; }
    }

    public interface IFilter
    {
        string CurrentFilter { get; set; }
    }

    public interface ISearch
    {
        string SearchString { get; set; }
    }
}
