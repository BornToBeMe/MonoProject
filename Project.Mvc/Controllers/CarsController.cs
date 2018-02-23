using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Service.DAL;
using Project.Service.Models;
using X.PagedList;

namespace Project.Mvc.Controllers
{
    public class CarsController : Controller
    {
        private CarContext db = new CarContext();

        // GET: Cars
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MakeSortParm = String.IsNullOrEmpty(sortOrder) ? "make_desc" : "";
            ViewBag.ModelSortParm = sortOrder == "Model" ? "model_desc" : "Model";

            var cars = db.Cars.Include(c => c.VehicleMake).Include(c => c.VehicleModel);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(m => m.VehicleMake.Name.Contains(searchString) || m.VehicleModel.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "make_desc":
                    cars = cars.OrderByDescending(m => m.VehicleMake.Name);
                    break;
                case "Model":
                    cars = cars.OrderBy(m => m.VehicleModel.Name);
                    break;
                case "model_desc":
                    cars = cars.OrderByDescending(m => m.VehicleModel.Name);
                    break;
                default:
                    cars = cars.OrderBy(m => m.VehicleMake.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(await cars.ToPagedListAsync(pageNumber, pageSize));
        }

        // GET: Cars/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.VehicleMakeID = new SelectList(db.VehicleMakes, "ID", "Name");
            ViewBag.VehicleModelID = new SelectList(db.VehicleModels, "VehicleModelID", "Name");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CarID,VehicleMakeID,VehicleModelID")] Car car)
        {
            try
            {
                if (ModelState.IsValid)
                            {
                                db.Cars.Add(car);
                                await db.SaveChangesAsync();
                                return RedirectToAction("Index");
                            }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            

            ViewBag.VehicleMakeID = new SelectList(db.VehicleMakes, "ID", "Name", car.VehicleMakeID);
            ViewBag.VehicleModelID = new SelectList(db.VehicleModels, "VehicleModelID", "Name", car.VehicleModelID);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleMakeID = new SelectList(db.VehicleMakes, "ID", "Name", car.VehicleMakeID);
            ViewBag.VehicleModelID = new SelectList(db.VehicleModels, "VehicleModelID", "Name", car.VehicleModelID);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CarID,VehicleMakeID,VehicleModelID")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.VehicleMakeID = new SelectList(db.VehicleMakes, "ID", "Name", car.VehicleMakeID);
            ViewBag.VehicleModelID = new SelectList(db.VehicleModels, "VehicleModelID", "Name", car.VehicleModelID);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again.";
            }
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Car car = await db.Cars.FindAsync(id);
            db.Cars.Remove(car);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
