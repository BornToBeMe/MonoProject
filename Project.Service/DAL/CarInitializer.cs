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
                new VehicleModel{Name="Ceed", Abrv="Ceed", VehicleMakeId=1},
                new VehicleModel{Name="Passat", Abrv="Passat", VehicleMakeId=2},
                new VehicleModel{Name="Golf", Abrv="Golf", VehicleMakeId=2},
                new VehicleModel{Name="Astra", Abrv="Astra", VehicleMakeId=3},
                new VehicleModel{Name="X5", Abrv="X5", VehicleMakeId=4},
                new VehicleModel{Name="E-Class", Abrv="E-Class", VehicleMakeId=5}
            };
            models.ForEach(m => context.VehicleModels.Add(m));
            context.SaveChanges();
        }
    }
}
