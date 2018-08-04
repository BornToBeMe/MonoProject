using Project.Model.Common;
using Project.Model;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using Project.Repository.Common;
using Project.Repository;
using X.PagedList;

namespace Project.WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class VehicleMakeController : ApiController
    {
        #region Constructors

        public VehicleMakeController(IMakeService service)
        {
            this.Service = service;
        }

        #endregion Constructors

        #region Properties

        protected IMakeService Service { get; private set; }

        #endregion Properties

        #region Methods
        
        [HttpGet]
        // GET: api/VehicleMake
        public async Task<IPagedList<IVehicleMake>> GetAllAsync(string sortBy, string currentFilter, string searchString, int? page, int? pageSize, bool ascending = true)
        {
            ISorting sorting = new Sorting();
            ISearch search = new Search();
            IPaging paging = new Paging();

            sorting.SortBy = sortBy;
            sorting.SortAscending = ascending;
            search.CurrentFilter = currentFilter;
            paging.PageNumber = page;
            paging.PageSize = pageSize;

            return await Service.SelectAllAsync(sorting, search, paging);
        }

        // GET: api/VehicleMake/5
        public async Task<IVehicleMake> GetAsync(Guid id)
        {
            return await Service.SelectByIDAsync(id);
        }

        // POST: api/VehicleMake
        public async Task<bool> PostMakeAsync(VehicleMake vehicleMake)
        {
            return await Service.CreateAsync(vehicleMake);
        }

        // PUT: api/VehicleMake/5
        public async Task<bool> PutMakeAsync(Guid id, VehicleMake vehicleMake)
        {
            return await Service.EditAsync(id, vehicleMake);
        }

        // DELETE: api/VehicleMake/5
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Service.DeleteAsync(id);
        }

        #endregion Methods
    }
}
