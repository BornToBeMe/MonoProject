using Project.Model.Common;
using Project.Model;
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
            return Repository.GetAllVehicleMakes();
        }

        public IVehicleMake GetVehicleMake(Guid id)
        {
            return Repository.GetVehicleMake(id);
        }

        public bool PutVehicleMake(Guid id, Model.VehicleMake vehicleMake)
        {
            return Repository.PutVehicleMake(id, vehicleMake);
        }

        public bool PostVehicleMake(VehicleMake obj)
        {
            return Repository.PostVehicleMake(obj);
        }

        public bool DeleteVehicleMake(Guid id)
        {
            return Repository.DeleteVehicleMake(id);
        }

        public List<IVehicleModel> GetAllVehicleModels()
        {
            return Repository.GetAllVehicleModels().ToList();
        }

        public IVehicleModel GetVehicleModel(Guid id)
        {
            return Repository.GetVehicleModel(id);
        }

        public IVehicleModel PutVehicleModel(Guid id, IVehicleModel vehicleModel)
        {
            return Repository.PutVehicleModel(id, vehicleModel);
        }

        public bool PostVehicleModel(IVehicleModel obj)
        {
            return Repository.PostVehicleModel(obj);
        }

        public bool DeleteVehicleModel(Guid id)
        {
            return Repository.DeleteVehicleModel(id);
        }
    }
}
