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
using AutoMapper;
using Project.Mvc.ViewModels;

namespace Project.Mvc.Controllers
{
    public class VehicleMakesController : Controller
    {
        IVehicleMakeService service = new VehicleMakeService();
        ISorting sorting = new Sorting();
        ISearch search = new Search();
        IPaging paging = new Paging();
         

        // GET: VehicleMakes
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            sorting.SortOrder = sortOrder;
            search.CurrentFilter = currentFilter;
            paging.PageNumber = page;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AbrvSortParm = sortOrder == "Abrv" ? "abrv_desc" : "Abrv";
            ViewBag.CurrentSort = sortOrder;

            IPagedList<VehicleMake> data = await service.SelectAllAsync(sorting, search, paging);
            return View(data);
        }

        // GET: VehicleMakes/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            VehicleMake data = await service.SelectByIDAsync(id);
            var dest = Mapper.Map<MakeVM>(data);
            return View(dest);
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
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Abrv")] MakeVM makeVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dest = Mapper.Map<VehicleMake>(makeVM);
                    await service.InsertAsync(dest);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(makeVM);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null && id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMake vehicleMake = await service.SelectByIDAsync(id.Value);
            var dest = Mapper.Map<MakeVM>(vehicleMake);
            if (dest == null)
            {
                return HttpNotFound();
            }

            return View(dest);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Abrv")] Guid id, MakeVM makeVM)
        {
            if (ModelState.IsValid)
            {
                var dest = Mapper.Map<VehicleMake>(makeVM);
                await service.UpdateAsync(id, dest);
                return RedirectToAction("Index");
            }
            return View(makeVM);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            VehicleMake vehicleMake = await service.SelectByIDAsync(id.Value);
            var dest = Mapper.Map<MakeVM>(vehicleMake);
            if (dest == null)
            {
                return HttpNotFound();
            }
            return View(dest);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                if(id != null)
                {
                    var delete = await service.DeleteAsync(id);
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
