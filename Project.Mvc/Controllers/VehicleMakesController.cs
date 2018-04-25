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
using Project.Service;
using Project.Service.Services;
using X.PagedList;
using AutoMapper;
using Project.Mvc.ViewModels;

namespace Project.Mvc.Controllers
{
    public class VehicleMakesController : Controller
    {
        IVehicleMakeService service = new VehicleMakeService();

        // GET: VehicleMakes
        public async Task<ActionResult> IndexAsync(string sortBy, string currentFilter, string searchString, int? page, int? pageSize, bool ascending = true)
        {
            ISorting sorting = new Sorting();
            ISearch search = new Search();
            IPaging paging = new Paging();

            sorting.SortBy = sortBy;
            sorting.SortAscending = ascending;
            search.CurrentFilter = currentFilter;
            paging.PageNumber = page;
            paging.PageSize = pageSize;

            ViewBag.Ascending = !ascending;
            ViewBag.CurrentSort = sortBy;

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="3", Text="3"},
                new SelectListItem() { Value="5", Text="5"},
                new SelectListItem() { Value="10", Text="10"}
            };

            ViewBag.SizeofPage = (pageSize ?? 3);
            ViewBag.CurrentFilter = currentFilter;

            IPagedList<VehicleMake> data = await service.SelectAllAsync(sorting, search, paging);
            return View(data);
        }

        // GET: VehicleMakes/Details/5
        public async Task<ActionResult> DetailsAsync(Guid? id)
        {
            return await ViewPageAsync(id);
        }

        // GET: VehicleMakes/Create
        [ActionName("CreateAsync")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleMakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(Include = "Id,Name,Abrv")] MakeVM makeVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dest = Mapper.Map<VehicleMake>(makeVM);
                    await service.CreateAsync(dest);
                    return RedirectToAction("");
                }
            }
            catch (Exception ex)
            {
                // throw ex;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
            return View(makeVM);
        }

        // GET: VehicleMakes/Edit/5
        public async Task<ActionResult> EditAsync(Guid? id)
        {
            return await ViewPageAsync(id);
        }

        // POST: VehicleMakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind(Include = "Id,Name,Abrv")] Guid id, MakeVM makeVM)
        {
            if (ModelState.IsValid)
            {
                var dest = Mapper.Map<VehicleMake>(makeVM);
                await service.EditAsync(id, dest);
                return RedirectToAction("");
            }
            return View(makeVM);
        }

        // GET: VehicleMakes/Delete/5
        public async Task<ActionResult> DeleteAsync(Guid? id)
        {
            return await ViewPageAsync(id);
        }

        // POST: VehicleMakes/Delete/5
        [HttpPost, ActionName("DeleteAsync")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync(Guid id)
        {
            try
            {
                if(id != null)
                {
                    await service.DeleteAsync(id);
                    return RedirectToAction("");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                // throw ex;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }    
        }

        public async Task<ActionResult> ViewPageAsync(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Id cannot be null");
            }
            if (id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            VehicleMake vehicleMake = await service.SelectByIDAsync(id.Value);
            var dest = Mapper.Map<MakeVM>(vehicleMake);
            if (dest == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(dest);
        }
    }
}
