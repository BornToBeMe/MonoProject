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
    public class VehicleMakesController : Controller
    {
        private CarContext db = new CarContext();

        // GET: VehicleMakes
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AbrvSortParm = sortOrder == "Abrv" ? "abrv_desc" : "Abrv";
            var makes = from m in db.VehicleMakes select m;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                makes = makes.Where(m => m.Name.Contains(searchString) || m.Abrv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    makes = makes.OrderByDescending(m => m.Name);
                    break;
                case "Abrv":
                    makes = makes.OrderBy(m => m.Abrv);
                    break;
                case "abrv_desc":
                    makes = makes.OrderByDescending(m => m.Abrv);
                    break;
                default:
                    makes = makes.OrderBy(m => m.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(await makes.ToPagedListAsync(pageNumber, pageSize));
        }

        // GET: VehicleMakes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await db.VehicleMakes.FindAsync(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                db.VehicleMakes.Add(vehicleMake);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(vehicleMake);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await db.VehicleMakes.FindAsync(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleMake).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await db.VehicleMakes.FindAsync(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VehicleMake vehicleMake = await db.VehicleMakes.FindAsync(id);
            db.VehicleMakes.Remove(vehicleMake);
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
