using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebAPI.RestModels
{
    /// <summary>
    /// Gets or sets the GetAll response needed for Frontend.
    /// </summary>
    /// <value>Items, TotalCount, PageNumber, PageSize, TotalPageCount.</value>
    public class VehicleResponse<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPageCount { get; set; }
    }
}