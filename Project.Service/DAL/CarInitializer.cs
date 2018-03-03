using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service.Models;

namespace Project.Service.DAL
{
    public class CarInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CarContext>
    {
        protected override void Seed(CarContext context)
        {
            var makes = new List<VehicleMake>
            {
                new VehicleMake{Name="Kia", Abrv="Kia", ID = Guid.NewGuid()},
                new VehicleMake{Name="Volkswagen", Abrv="VW", ID = Guid.NewGuid()},
                new VehicleMake{Name="Opel", Abrv="Opel", ID = Guid.NewGuid()},
                new VehicleMake{Name="BMW", Abrv="BMW", ID = Guid.NewGuid()},
                new VehicleMake{Name="Mercedes", Abrv="Mercedes", ID = Guid.NewGuid()}
            };
            makes.ForEach(m => context.VehicleMakes.Add(m));
            context.SaveChanges();

            var models = new List<VehicleModel>
            {
                new VehicleModel{Name="Ceed", Abrv="Ceed", VehicleModelId = Guid.NewGuid()},
                new VehicleModel{Name="Passat", Abrv="Passat", VehicleModelId = Guid.NewGuid()},
                new VehicleModel{Name="Golf", Abrv="Golf", VehicleModelId = Guid.NewGuid()},
                new VehicleModel{Name="Astra", Abrv="Astra", VehicleModelId = Guid.NewGuid()},
                new VehicleModel{Name="X5", Abrv="X5", VehicleModelId = Guid.NewGuid()},
                new VehicleModel{Name="E-Class", Abrv="E-Class", VehicleModelId = Guid.NewGuid()}
            };
            models.ForEach(m => context.VehicleModels.Add(m));
            context.SaveChanges();
        }
    }
}
