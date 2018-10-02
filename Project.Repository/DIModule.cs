using AutoMapper;
using Ninject;
using Project.DAL;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;
using System;

namespace Project.Repository
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleMake>().To<Model.VehicleMake>();
            Bind<IVehicleModel>().To<Model.VehicleModel>();
            Bind<ICarContext>().To<CarContext>().InSingletonScope();
            Bind<IMakeRepository>().To<MakeRepository>();
            Bind<IModelRepository>().To<ModelRepository>();

        }
    }
}
