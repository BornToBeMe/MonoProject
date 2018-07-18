using Project.Model.Common;
using Project.Repository.Common;
using Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Project.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        public ProjectRepository(ICarContext context)
        {
            this.Context = context;
        }

        protected ICarContext Context { get; private set; }

        public List<IVehicleMake> GetAllVehicleMakes()
        {
            var bla = Context.VehicleMakes.ToList();
            var bla1 = Mapper.Map<List<IVehicleMake>>(bla);
            return bla1;
        }

        public List<IVehicleModel> GetAllVehicleModels()
        {
            return new List<IVehicleModel>(Mapper.Map<List<Project.Model.VehicleModel>>(Context.VehicleModels));
        }
    }
}
