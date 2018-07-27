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
            return Mapper.Map<List<IVehicleMake>>(Context.VehicleMakes);
        }

        public List<IVehicleModel> GetAllVehicleModels()
        {
            return Mapper.Map<List<IVehicleModel>>(Context.VehicleModels);
        }
    }
}
