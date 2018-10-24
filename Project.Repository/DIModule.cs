using AutoMapper;
using Ninject;
using Ninject.Extensions.Factory;
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
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            Bind<IUnitOfWorkFactory>().ToFactory();
        }
    }
}
