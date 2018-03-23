using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Project.Service.Models;
using Project.Service.ViewModels;

namespace Project.Mvc
{
    public static class MapperConfig
    {
        public static void SetAutomapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<VehicleMake, MakeVM>();
            });
        }
    }
}