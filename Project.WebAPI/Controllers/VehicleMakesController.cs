using Project.Model.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.WebAPI.Controllers
{
    public class VehicleMakesController : ApiController
    {
        #region Constructors

        public VehicleMakesController(IProjectService service)
        {
            this.Service = service;
        }

        #endregion Constructors

        #region Properties

        protected IProjectService Service { get; private set; }

        #endregion Properties

        #region Methods

        // GET: api/VehicleMakes
        public List<IVehicleMake> Get()
        {
            return Service.GetAllVehicleMakes();
        }

        // GET: api/VehicleMakes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/VehicleMakes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/VehicleMakes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/VehicleMakes/5
        public void Delete(int id)
        {
        }

        #endregion Methods
    }
}
