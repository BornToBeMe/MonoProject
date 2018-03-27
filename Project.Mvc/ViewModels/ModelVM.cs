using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Mvc.ViewModels
{
    public class ModelVM
    {
        public Guid VehicleModelId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public Guid MakeVMId { get; set; }

        public virtual MakeVM MakeVM { get; set; }
    }
}