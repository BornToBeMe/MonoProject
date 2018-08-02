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
    public class VehicleModelController : ApiController
    {
        #region Constructors

        public VehicleModelController(IProjectService service)
        {
            this.Service = service;
        }

        #endregion Constructors

        #region Properties

        protected IProjectService Service { get; private set; }

        #endregion Properties

        // GET: api/VehicleModel
        public List<IVehicleModel> Get()
        {
            return Service.GetAllVehicleModels();
        }

        public IVehicleModel GetVehicleModel(Guid id)
        {
            return Service.GetVehicleModel(id);
        }

        public IVehicleModel PutVehicleModel(Guid id, IVehicleModel vehicleModel)
        {
            return Service.PutVehicleModel(id, vehicleModel);
        }

        public bool PostVehicleModel(IVehicleModel obj)
        {
            return Service.PostVehicleModel(obj);
        }

        public bool DeleteVehicleModel(Guid id)
        {
            return Service.DeleteVehicleModel(id);
        }

    }
}
