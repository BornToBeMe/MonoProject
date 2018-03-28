using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Project.Mvc.ViewModels;
using Project.Service.Models;

namespace Project.Mvc
{
    public static class MapperConfig
    {
        public static void SetAutomapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<VehicleMake, MakeVM>().ReverseMap();
                cfg.CreateMap<VehicleModel, ModelVM>().ForMember(d => d.MakeVM, opt => opt.MapFrom(s => s)).ReverseMap();
            });
        }
    }
}