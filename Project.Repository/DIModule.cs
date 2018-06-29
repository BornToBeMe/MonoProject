﻿using AutoMapper;
using Project.DAL;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service;
using Project.Service.Common;
using System;

namespace Project.Repository
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DAL.VehicleMake, Model.VehicleMake>().ReverseMap();
                cfg.CreateMap<DAL.VehicleMake, Model.Common.IVehicleMake>().ReverseMap();
                cfg.CreateMap<Model.Common.IVehicleMake, Model.VehicleMake>().ReverseMap();
                cfg.CreateMap<DAL.VehicleModel, Model.VehicleModel>().ReverseMap();
                cfg.CreateMap<DAL.VehicleModel, Model.Common.IVehicleModel>().ReverseMap();
                cfg.CreateMap<Model.Common.IVehicleModel, Model.VehicleModel>().ReverseMap();
            });

            Bind<ICarContext>().To<CarContext>().InSingletonScope();
            Bind<IProjectRepository>().To<ProjectRepository>();
        }
    }
}