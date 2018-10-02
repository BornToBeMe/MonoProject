using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Project.WebAPI.Controllers.VehicleMakeController;

namespace Project.WebAPI.App_Start
{
    public static class MapperConfig
    {
        public static void SetAutomapper()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<IVehicleMake, VehicleMakeViewModel>()
                    .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Abrv, opts => opts.MapFrom(src => src.Abrv))
                    .ReverseMap();
                // cfg.CreateMap<IVehicleModel, VehicleModelViewModel>().ReverseMap();
            });
        }
    }
}