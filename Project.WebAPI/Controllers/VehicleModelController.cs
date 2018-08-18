using Newtonsoft.Json;
using Project.Common;
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

        public class CallDetails
        {
            [JsonProperty("sortBy")]
            public string Sort { get; set; }
            [JsonProperty("currentFilter")]
            public string Filter { get; set; }
            [JsonProperty("page")]
            public int Page { get; set; }
            [JsonProperty("pageSize")]
            public int PageSize { get; set; }
            [JsonProperty("ascending")]
            public bool Ascending { get; set; }
        }

        #endregion Properties

        #region Methods

        [HttpGet]
        // GET: api/VehicleModel
        public async Task<IPagedList<IVehicleModel>> GetAllAsync([FromUri]CallDetails callDetails)
        {
            ISorting sorting = new Sorting();
            ISearch search = new Search();
            IPaging paging = new Paging();

            sorting.SortBy = callDetails.Sort;
            sorting.SortAscending = callDetails.Ascending;
            search.CurrentFilter = callDetails.Filter;
            paging.PageNumber = callDetails.Page;
            paging.PageSize = callDetails.PageSize;

            return await Service.SelectAllAsync(sorting, search, paging);
        }

        // GET: api/VehicleModel/5
        public async Task<IVehicleModel> GetAsync(Guid id)
        {
            return await Service.SelectByIDAsync(id);
        }

        // POST: api/VehicleModel
        public async Task<IHttpActionResult> PostModelAsync(VehicleModel vehicleModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Service.CreateAsync(vehicleModel);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(vehicleModel);
        }

        // PUT: api/VehicleModel/5
        public async Task<IHttpActionResult> PutModelAsync(Guid id, VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                await Service.EditAsync(id, vehicleModel);
            }
            return Ok(vehicleModel);
        }

        // DELETE: api/VehicleModel/5
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            try
            {
                if (id != null)
                {
                    await Service.DeleteAsync(id);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok();
        }

        #endregion Methods
    }
}
