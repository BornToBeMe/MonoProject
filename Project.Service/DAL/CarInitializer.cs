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
                new VehicleMake{Name="Kia", Abrv="Kia"},
                new VehicleMake{Name="Volkswagen", Abrv="VW"},
                new VehicleMake{Name="Opel", Abrv="Opel"},
                new VehicleMake{Name="BMW", Abrv="BMW"},
                new VehicleMake{Name="Mercedes", Abrv="Mercedes"}
            };
            makes.ForEach(m => context.VehicleMakes.Add(m));
            context.SaveChanges();

            var models = new List<VehicleModel>
            {
                new VehicleModel{Name="Ceed", Abrv="Ceed", VehicleModelID=1},
                new VehicleModel{Name="Passat", Abrv="Passat", VehicleModelID=2},
                new VehicleModel{Name="Golf", Abrv="Golf", VehicleModelID=3},
                new VehicleModel{Name="Astra", Abrv="Astra", VehicleModelID=4},
                new VehicleModel{Name="X5", Abrv="X5", VehicleModelID=5},
                new VehicleModel{Name="E-Class", Abrv="E-Class", VehicleModelID=6}
            };
            models.ForEach(m => context.VehicleModels.Add(m));
            context.SaveChanges();

            var cars = new List<Car>
            {
                new Car{VehicleMakeID=1, VehicleModelID=1},
                new Car{VehicleMakeID=2, VehicleModelID=2},
                new Car{VehicleMakeID=2, VehicleModelID=3},
                new Car{VehicleMakeID=3, VehicleModelID=4},
                new Car{VehicleMakeID=4, VehicleModelID=5},
                new Car{VehicleMakeID=5, VehicleModelID=6}
            };
            cars.ForEach(c => context.Cars.Add(c));
            context.SaveChanges();
        }
    }
}
