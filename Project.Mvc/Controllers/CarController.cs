using Project.Service;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.Mvc.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        public async Task<ActionResult> Index()
        {
            List<Car> car = await CarHelper.SelectAllAsync();
            return View(car);
        }

        // GET: Car/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Car car = await CarHelper.SelectByIDAsync(id);
            return View(car);
        }

        // GET: Car/Create
        public async Task<ActionResult> Create()
        {
            //Car car = await CarHelper.InsertAsync(Car);
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Car/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
