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
            [JsonProperty("page")]
            public int Page { get; set; }
            [JsonProperty("pageSize")]
            public int PageSize { get; set; }
            [JsonProperty("ascending")]
            public bool Ascending { get; set; }
        }

        public class VehicleMakeViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Abrv { get; set; }
        }

        public class VehicleMakeResponse
        {
            public IEnumerable<IVehicleMake> Items { get; set; }
            public int TotalCount { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalPageCount { get; set; }
        }

        #endregion Properties

        #region Methods
        
        [HttpGet]
        // GET: api/VehicleMake
        public async Task<IHttpActionResult> GetAllAsync([FromUri]CallDetails callDetails)
        {
            ISorting sorting = new Sorting();
            ISearch search = new Search();
            IPaging paging = new Paging();

            sorting.SortBy = callDetails.Sort;
            sorting.SortAscending = callDetails.Ascending;
            search.CurrentFilter = callDetails.Filter;
            paging.PageNumber = callDetails.Page;
            paging.PageSize = callDetails.PageSize;

            var i = await Service.SelectAllAsync(sorting, search, paging);

            return Ok(new VehicleMakeResponse
            {
                Items = i,
                TotalCount = i.TotalItemCount,
                PageNumber = i.PageNumber,
                PageSize = i.PageSize,
                TotalPageCount = i.PageCount
            });
        }

        // GET: api/VehicleMake/5
        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            try
            {
                if (id != null)
                {
                    await Service.SelectByIDAsync(id);
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
            return Ok(await Service.SelectByIDAsync(id));
        }

        // POST: api/VehicleMake
        public async Task<IHttpActionResult> PostMakeAsync(VehicleMakeViewModel vehicleMake)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dest = AutoMapper.Mapper.Map<VehicleMake>(vehicleMake);
                    await Service.CreateAsync(dest);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
            return Ok(vehicleMake);
        }

        // PUT: api/VehicleMake/5
        public async Task<IHttpActionResult> PutMakeAsync(Guid id, VehicleMakeViewModel vehicleMake)
        {
            if (ModelState.IsValid)
            {
                var dest = AutoMapper.Mapper.Map<IVehicleMake>(vehicleMake);
                await Service.EditAsync(id, dest);
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
