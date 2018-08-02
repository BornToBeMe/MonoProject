using Project.Model.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Project.WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class VehicleMakeController : ApiController
    {
        #region Constructors

        public VehicleMakeController(IProjectService service)
        {
            this.Service = service;
        }

        #endregion Constructors

        #region Properties

        protected IProjectService Service { get; private set; }

        #endregion Properties

        #region Methods
        
        [HttpGet]
        // GET: api/VehicleMake
        public List<IVehicleMake> Get()
        {
            return Service.GetAllVehicleMakes();
        }

        public IVehicleMake GetVehicleMake(Guid id)
        {
            return Service.GetVehicleMake(id);
        }

        public IVehicleMake PutVehicleMake(Guid id, IVehicleMake vehicleMake)
        {
            return Service.PutVehicleMake(id, vehicleMake);
        }

        public bool PostVehicleMake(IVehicleMake obj)
        {
            return Service.PostVehicleMake(obj);
        }

        public bool DeleteVehicleMake(Guid id)
        {
            return Service.DeleteVehicleMake(id);
        }

        #endregion Methods
    }
}
