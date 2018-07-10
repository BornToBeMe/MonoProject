using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{

    public class ProjectService : IProjectService
    {
        public ProjectService(IProjectRepository repository)
        {
            this.Repository = repository;
        }

        protected IProjectRepository Repository { get; private set; }

        public List<IVehicleMake> GetAllVehicleMakes()
        {
            return Repository.GetAllVehicleMakes().ToList();
        }

        public List<IVehicleModel> GetAllVehicleModels()
        {
            return Repository.GetAllVehicleModels().ToList();
        }
    }
}
