using Project.Model.Common;
using Project.Repository.Common;
using Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new List<IVehicleMake>(AutoMapper.Mapper.Map<List<Project.Model.VehicleMake>>(Context.VehicleMakes));
        }

        public List<IVehicleModel> GetAllVehicleModels()
        {
            return new List<IVehicleModel>(AutoMapper.Mapper.Map<List<Project.Model.VehicleModel>>(Context.VehicleModels));
        }
    }
}
