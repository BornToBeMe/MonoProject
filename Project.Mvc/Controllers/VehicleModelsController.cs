﻿using System;
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
using Project.Service.Services;

namespace Project.Mvc.Controllers
{
    public class VehicleModelsController : Controller
    {
        CarContext db = new CarContext();
        IVehicleModelService service = new VehicleModelService();

        // GET: VehicleModels
        public async Task<ActionResult> Index(Sorting sortOrder, Service.Services.Filter currentFilter, Search searchString, Paging paging)
        {
            ViewBag.CurrentSort = sortOrder.SortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder.SortOrder) ? "name_desc" : "";
            ViewBag.AbrvSortParm = sortOrder.SortOrder == "Abrv" ? "abrv_desc" : "Abrv";

            IPagedList<VehicleModel> data = await service.SelectAllAsync(sortOrder, currentFilter, searchString, paging);
            return View(data);
        }

        // GET: VehicleModels/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            VehicleModel data = await service.SelectByIDAsync(id);
            return View(data);
        }

        // GET: VehicleModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VehicleModelId,VehicleMakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                await service.InsertAsync(vehicleModel);
                return RedirectToAction("Index");
            }

            return View(vehicleModel);
        }

        // GET: VehicleModels/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await db.VehicleModels.FindAsync(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VehicleModelId,VehicleMakeId,Name,Abrv")] Guid id, VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateAsync(id, vehicleModel);
                return RedirectToAction("Index");
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModel vehicleModel = await db.VehicleModels.FindAsync(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleModels/Delete/5
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
