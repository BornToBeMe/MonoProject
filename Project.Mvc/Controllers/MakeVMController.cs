using AutoMapper;
using Project.Service.DAL;
using Project.Service.Models;
using Project.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Mvc.Controllers
{
    public class MakeVMController : Controller
    {
        // GET: MakeVM
        public ActionResult Index()
        {
            using(var context = new CarContext())
            {
                var makes = context.VehicleMakes.ToList();

                var dest = Mapper.Map<List<MakeVM>>(makes);

                return View(dest);
            }
        }
    }
}