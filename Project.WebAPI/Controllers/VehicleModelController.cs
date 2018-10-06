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

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleModelController" /> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public VehicleModelController(IModelService service)
        {
            this.Service = service;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <value>The service.</value>
        protected IModelService Service { get; private set; }

        /// <summary>
        /// Gets or sets the calls for GetAll.
        /// </summary>
        /// <value>Sort, Filter, Page, PageSize, Ascending.</value>
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

        /// <summary>
        /// Gets or sets the View Model.
        /// </summary>
        /// <value>Name, Abrv, VehicleMakeId</value>
        public class VehicleModelViewModel
        {
            public Guid VehicleModelId { get; set; }
            public string Name { get; set; }
            public string Abrv { get; set; }
            public Guid VehicleMakeId { get; set; }
        }

        /// <summary>
        /// Gets or sets the GetAll response needed for Frontend.
        /// </summary>
        /// <value>Items, MakeList, TotalCount, PageNumber, PageSize, TotalPageCount.</value>
        public class VehicleModelResponse
        {
            public IEnumerable<IVehicleModel> Items { get; set; }
            public IList<IVehicleMake> MakeList { get; set; }
            public int TotalCount { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalPageCount { get; set; }
        }

        #endregion Properties

        #region Methods

        [HttpGet]
        // GET: api/VehicleModel
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

            var list = await Service.PopulateMakesDropDownList();
            var i = await Service.SelectAllAsync(sorting, search, paging);

            return Ok(new VehicleModelResponse
            {
                Items = i,
                MakeList = list,
                TotalCount = i.TotalItemCount,
                PageNumber = i.PageNumber,
                PageSize = i.PageSize,
                TotalPageCount = i.PageCount
            });
        }

        // GET: api/VehicleModel/5
        public async Task<IVehicleModel> GetAsync(Guid id)
        {
            return await Service.SelectByIDAsync(id);
        }

        // POST: api/VehicleModel
        public async Task<IHttpActionResult> PostModelAsync(VehicleModelViewModel vehicleModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dest = AutoMapper.Mapper.Map<IVehicleModel>(vehicleModel);
                    await Service.CreateAsync(dest);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(vehicleModel);
        }

        // PUT: api/VehicleModel/5
        public async Task<IHttpActionResult> PutModelAsync(Guid id, VehicleModelViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                var dest = AutoMapper.Mapper.Map<IVehicleModel>(vehicleModel);
                await Service.EditAsync(id, dest);
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
