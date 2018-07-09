﻿using Project.Model.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.WebAPI.Controllers
{
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

        #endregion Methods
    }
}