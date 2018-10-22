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
using Project.WebAPI.RestModels;

namespace Project.WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class VehicleMakeController : ApiController
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleMakeController" /> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public VehicleMakeController(IMakeService service)
        {
            this.Service = service;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <value>The service.</value>
        protected IMakeService Service { get; private set; }

        /// <summary>
        /// Gets or sets the View Model.
        /// </summary>
        /// <value>Name, Abrv.</value>
        public class VehicleMakeViewModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Abrv { get; set; }
        }

        #endregion Properties

        #region Methods
        
        [HttpGet]
        // GET: api/VehicleMake
        public async Task<IHttpActionResult> SelectAsync([FromUri]CallDetails callDetails)
        {
            ISorting sorting = new Sorting();
            ISearch search = new Search();
            IPaging paging = new Paging();

            sorting.SortBy = callDetails.Sort;
            sorting.SortAscending = callDetails.Ascending;
            search.CurrentFilter = callDetails.Filter;
            paging.PageNumber = callDetails.Page;
            paging.PageSize = callDetails.PageSize;

            var i = await Service.SelectAsync(sorting, search, paging);

            return Ok(new VehicleResponse<IVehicleMake>
            {
                Items = i,
                TotalCount = i.TotalItemCount,
                PageNumber = i.PageNumber,
                PageSize = i.PageSize,
                TotalPageCount = i.PageCount
            });
        }

        // GET: api/VehicleMake/5
        public async Task<IHttpActionResult> SelectByIdAsync(Guid id)
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
        public async Task<IHttpActionResult> InsertAsync(VehicleMakeViewModel vehicleMake)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dest = AutoMapper.Mapper.Map<IVehicleMake>(vehicleMake);
                    await Service.InsertAsync(dest);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
            return Ok(vehicleMake);
        }

        // PUT: api/VehicleMake/5
        public async Task<IHttpActionResult> UpdateAsync(Guid id, VehicleMakeViewModel vehicleMake)
        {
            if (ModelState.IsValid)
            {
                var dest = AutoMapper.Mapper.Map<IVehicleMake>(vehicleMake);
                await Service.UpdateAsync(id, dest);
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
