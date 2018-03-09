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
using Project.Service.Services;
using X.PagedList;
using System.Data.Entity.Infrastructure;

namespace Project.Mvc.Controllers
{
    public class VehicleMakesController : Controller
    {
        CarContext db = new CarContext();
        IVehicleMakeService service = new VehicleMakeService();

        // GET: VehicleMakes
        public async Task<ActionResult> Index(Sorting sortOrder, Service.Services.Filter currentFilter, Search searchString, Paging pagination)
        {
            ViewBag.CurrentSort = sortOrder.SortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder.SortOrder) ? "name_desc" : "";
            ViewBag.AbrvSortParm = sortOrder.SortOrder == "Abrv" ? "abrv_desc" : "Abrv";

            IPagedList<VehicleMake> data = await service.SelectAllAsync(sortOrder, currentFilter, searchString, pagination);
            return View(data);
        }

        // GET: VehicleMakes/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            VehicleMake data = await service.SelectByIDAsync(id);
            return View(data);
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
            try
            {
                if (ModelState.IsValid)
                {
                    await service.InsertAsync(vehicleMake);
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again.");
            }

            return View(vehicleMake);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Abrv")] Guid id, VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateAsync(id, vehicleMake);
                return RedirectToAction("Index");
            }
            return View(vehicleMake);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
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
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await service.DeleteAsync(id);
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
