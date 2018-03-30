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
using AutoMapper;
using Project.Mvc.ViewModels;

namespace Project.Mvc.Controllers
{
    public class VehicleModelsController : Controller
    {
        IVehicleModelService service = new VehicleModelService();
        ISorting sorting = new Sorting();
        ISearch search = new Search();
        IPaging paging = new Paging();


        // GET: VehicleModels
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            sorting.SortOrder = sortOrder;
            search.CurrentFilter = currentFilter;
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            VehicleModel data = await service.SelectByIDAsync(id);
            var dest = Mapper.Map<ModelVM>(data);
            return View(dest);
        }

        // GET: VehicleModels/Create
        public ActionResult Create()
        {
            IList<VehicleMake> list = service.PopulateMakesDropDownList();
            ViewBag.Make = new SelectList(list, "Id", "Name");
            //ViewBag.VehicleMakeId = new SelectList(makeQuery, "Id", "Name", selectedMake);
            //PopulateMakesDropDownList();
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VehicleModelId,VehicleMakeId,Name,Abrv")] ModelVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dest = Mapper.Map<VehicleModel>(modelVM);
                    await service.InsertAsync(dest);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //PopulateMakesDropDownList(vehicleModel.VehicleMakeId);
            return View(modelVM);
        }

        // GET: VehicleModels/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IList<VehicleMake> list = service.PopulateMakesDropDownList();
            ViewBag.Make = new SelectList(list, "Id", "Name");
            VehicleModel vehicleModel = await service.SelectByIDAsync(id.Value);
            var dest = Mapper.Map<ModelVM>(vehicleModel);
            if (dest == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            //PopulateMakesDropDownList(vehicleModel.VehicleMakeId);
            return View(dest);
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VehicleModelId,VehicleMakeId,Name,Abrv")] Guid id, ModelVM modelVM)
        {
            if (ModelState.IsValid)
            {
                var dest = Mapper.Map<VehicleModel>(modelVM);
                await service.UpdateAsync(id, dest);

                return RedirectToAction("Index");
            }
            return View(modelVM);
        }

        // GET: VehicleModels/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            VehicleModel vehicleModel = await service.SelectByIDAsync(id.Value);
            var dest = Mapper.Map<ModelVM>(vehicleModel);
            if (dest == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(dest);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                if (id != null)
                {
                    await service.DeleteAsync(id);
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


    }
}
