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
using Project.Common;
using Newtonsoft.Json;

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

        public class CallDetails
        {
            [JsonProperty("sortBy")]
            public string Sort { get; set; }
            [JsonProperty("currentFilter")]
            public string Filter { get; set; }
            [JsonProperty("searchString")]
            public string Search { get; set; }
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
        // GET: api/VehicleMake
        public async Task<IPagedList<IVehicleMake>> GetAllAsync(CallDetails callDetails)
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

        // GET: api/VehicleMake/5
        public async Task<IVehicleMake> GetAsync(Guid id)
        {
            return await Service.SelectByIDAsync(id);
        }

        // POST: api/VehicleMake
        public async Task<IHttpActionResult> PostMakeAsync(VehicleMake vehicleMake)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Service.CreateAsync(vehicleMake);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
            return Ok(vehicleMake);
        }

        // PUT: api/VehicleMake/5
        public async Task<IHttpActionResult> PutMakeAsync(Guid id, VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                await Service.EditAsync(id, vehicleMake);
            }
            return Ok(vehicleMake);
        }

        // DELETE: api/VehicleMake/5
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
