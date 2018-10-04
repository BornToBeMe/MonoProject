using AutoMapper;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Project.WebAPI.Controllers.VehicleMakeController;
using static Project.WebAPI.Controllers.VehicleModelController;

namespace Project.WebAPI.App_Start
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<IVehicleMake, VehicleMakeViewModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Abrv, opts => opts.MapFrom(src => src.Abrv))
                .ReverseMap();
            CreateMap<IVehicleModel, VehicleModelViewModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Abrv, opts => opts.MapFrom(src => src.Abrv))
                .ForMember(dest => dest.VehicleMakeId, opts => opts.MapFrom(src => src.VehicleMakeId))
                .ReverseMap();
        }
    }
}