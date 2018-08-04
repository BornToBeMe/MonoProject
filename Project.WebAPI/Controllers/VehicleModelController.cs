using Project.Model;
using Project.Model.Common;
using Project.Repository;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using X.PagedList;

namespace Project.WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class VehicleModelController : ApiController
    {
        #region Constructors

        public VehicleModelController(IModelService service)
        {
            this.Service = service;
        }

        #endregion Constructors

        #region Properties

        protected IModelService Service { get; private set; }

        #endregion Properties

        #region Methods

        [HttpGet]
        // GET: api/VehicleModel
        public async Task<IPagedList<IVehicleModel>> GetAllAsync(string sortBy, string currentFilter, string searchString, int? page, int? pageSize, bool ascending = true)
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

        // GET: api/VehicleModel/5
        public async Task<IVehicleModel> GetAsync(Guid id)
        {
            return await Service.SelectByIDAsync(id);
        }

        // POST: api/VehicleModel
        public async Task<bool> PostModelAsync(VehicleModel vehicleModel)
        {
            return await Service.CreateAsync(vehicleModel);
        }

        // PUT: api/VehicleModel/5
        public async Task<bool> PutModelAsync(Guid id, VehicleModel vehicleModel)
        {
            return await Service.EditAsync(id, vehicleModel);
        }

        // DELETE: api/VehicleModel/5
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Service.DeleteAsync(id);
        }

        #endregion Methods
    }
}
