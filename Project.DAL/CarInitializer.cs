using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL
{
    public class CarInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CarContext>
    {
        /// <summary>
        /// Adds new items into database using CarContext
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(CarContext context)
        {
            var makes = new List<VehicleMake>
            {
                new VehicleMake{Name="Kia", Abrv="Kia", Id = Guid.NewGuid()},
                new VehicleMake{Name="Volkswagen", Abrv="VW", Id = Guid.NewGuid()},
                new VehicleMake{Name="Opel", Abrv="Opel", Id = Guid.NewGuid()},
                new VehicleMake{Name="BMW", Abrv="BMW", Id = Guid.NewGuid()},
                new VehicleMake{Name="Mercedes", Abrv="Mercedes", Id = Guid.NewGuid()}
            };
            makes.ForEach(m => context.VehicleMakes.Add(m));
            context.SaveChanges();

            var models = new List<VehicleModel>
            {
                new VehicleModel{Name="Ceed", Abrv="Ceed", VehicleModelId = Guid.NewGuid(), VehicleMakeId = makes.Single(s => s.Name == "Kia").Id},
                new VehicleModel{Name="Passat", Abrv="Passat", VehicleModelId = Guid.NewGuid(), VehicleMakeId = makes.Single(s => s.Name == "Volkswagen").Id},
                new VehicleModel{Name="Golf", Abrv="Golf", VehicleModelId = Guid.NewGuid(), VehicleMakeId = makes.Single(s => s.Name == "Volkswagen").Id},
                new VehicleModel{Name="Astra", Abrv="Astra", VehicleModelId = Guid.NewGuid(), VehicleMakeId = makes.Single(s => s.Name == "Opel").Id},
                new VehicleModel{Name="X5", Abrv="X5", VehicleModelId = Guid.NewGuid(), VehicleMakeId = makes.Single(s => s.Name == "BMW").Id},
                new VehicleModel{Name="E-Class", Abrv="E-Class", VehicleModelId = Guid.NewGuid(), VehicleMakeId = makes.Single(s => s.Name == "Mercedes").Id}
            };
            models.ForEach(m => context.VehicleModels.Add(m));
            context.SaveChanges();
        }
    }
}
