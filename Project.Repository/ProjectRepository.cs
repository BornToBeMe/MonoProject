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

        public IVehicleMake GetVehicleMake(Guid id)
        {
            return Mapper.Map<IVehicleMake>(Context.VehicleMakes.Find(id));
        }

        public bool PutVehicleMake(Guid id, IVehicleMake vehicleMake)
        {
            var entity = Context.VehicleMakes.Find(id);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            Context.Entry(entity).CurrentValues.SetValues(vehicleMake);

            return (Context.SaveChanges() > 0);
        }

        public bool PostVehicleMake(IVehicleMake obj)
        {
            var map = Mapper.Map<VehicleMake>(obj);
            map.Id = Guid.NewGuid();
            Context.VehicleMakes.Add(map);

            return (Context.SaveChanges() > 0);
        }

        public bool DeleteVehicleMake(Guid id)
        {
            VehicleMake existing = Context.VehicleMakes.Find(id);
            Context.VehicleMakes.Remove(existing);
            return (Context.SaveChanges() > 0);
        }

        public List<IVehicleModel> GetAllVehicleModels()
        {
            return Mapper.Map<List<IVehicleModel>>(Context.VehicleModels);
        }

        public IVehicleModel GetVehicleModel(Guid id)
        {
            return Mapper.Map<IVehicleModel>(Context.VehicleModels.Find(id));
        }

        public IVehicleModel PutVehicleModel(Guid id, IVehicleModel vehicleModel)
        {
            var map = Mapper.Map<IVehicleModel>((Context.VehicleModels).Single(c => c.VehicleModelId == id));
            if (map == null)
            {
                throw new ArgumentNullException();
            }
            Context.Entry(map).CurrentValues.SetValues(vehicleModel);
            Context.SaveChanges();

            return map;
        }

        public bool PostVehicleModel(IVehicleModel obj)
        {
            obj.VehicleModelId = Guid.NewGuid();
            //Context.VehicleModels.Add(obj);
            return (Context.SaveChanges() > 0);
        }

        public bool DeleteVehicleModel(Guid id)
        {
            VehicleModel existing = Context.VehicleModels.Find(id);
            Context.VehicleModels.Remove(existing);
            return (Context.SaveChanges() > 0);
        }
    }
}
