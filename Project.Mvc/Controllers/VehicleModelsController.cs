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
using Project.Service.Services;

namespace Project.Mvc.Controllers
{
    public class VehicleModelsController : Controller
    {
        CarContext db = new CarContext();
        IVehicleModelService service = new VehicleModelService();
        ISorting sorting = new Sorting();
        ISearch search = new Search();
        IPaging paging = new Paging();


        // GET: VehicleModels
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            sorting.SortOrder = sortOrder;
            search.CurrentFilter = currentFilter;
            search.SearchString = searchString;
            paging.PageNumber = page;

            ViewBag.MakeSortParm = String.IsNullOrEmpty(sortOrder) ? "make_desc" : "";
            ViewBag.AbrvSortParm = sortOrder == "Abrv" ? "abrv_desc" : "Abrv";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CurrentSort = sortOrder;

            IPagedList<VehicleModel> data = await service.SelectAllAsync(sorting, search, paging);
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
            PopulateMakesDropDownList();
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
            PopulateMakesDropDownList(vehicleModel.VehicleMakeId);
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
            PopulateMakesDropDownList(vehicleModel.VehicleMakeId);
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

        private void PopulateMakesDropDownList(object selectedMake = null)
        {
            var makeQuery = from d in db.VehicleMakes orderby d.Name select d;
            ViewBag.VehicleMakeId = new SelectList(makeQuery, "Id", "Name", selectedMake);
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
