using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<DAL.VehicleMake, Model.VehicleMake>().ReverseMap();
            CreateMap<DAL.VehicleMake, Model.Common.IVehicleMake>().ReverseMap();
            CreateMap<Model.Common.IVehicleMake, Model.VehicleMake>().ReverseMap();
            CreateMap<DAL.VehicleModel, Model.VehicleModel>().ReverseMap();
            CreateMap<DAL.VehicleModel, Model.Common.IVehicleModel>().ReverseMap();
            CreateMap<Model.Common.IVehicleModel, Model.VehicleModel>().ReverseMap();
        }
    }
}
