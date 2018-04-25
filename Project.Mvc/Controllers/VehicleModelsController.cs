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
using X.PagedList;
using Project.Service.Services;
using AutoMapper;
using Project.Mvc.ViewModels;

namespace Project.Mvc.Controllers
{
    public class VehicleModelsController : Controller
    {
        IVehicleModelService service = new VehicleModelService();

        // GET: VehicleModels
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

            ViewBag.pageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="3", Text="3"},
                new SelectListItem() { Value="5", Text="5"},
                new SelectListItem() { Value="10", Text="10"}
            };

            ViewBag.Size = (pageSize ?? 3);
            ViewBag.CurrentFilter = currentFilter;

            IPagedList<VehicleModel> data = await service.SelectAllAsync(sorting, search, paging);
            return View(data);
        }

        // GET: VehicleModels/Details/5
        public async Task<ActionResult> DetailsAsync(Guid? id)
        {
            return await ViewPageAsync(id);
        }

        // GET: VehicleModels/Create
        [ActionName("CreateAsync")]
        public ActionResult Create()
        {
            ViewBag.Make = service.PopulateMakesDropDownList();
            return View();
        }

        // POST: VehicleModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(Include = "VehicleModelId,VehicleMakeId,Name,Abrv")] ModelVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dest = Mapper.Map<VehicleModel>(modelVM);
                    await service.CreateAsync(dest);
                    return RedirectToAction("");
                }
            }
            catch (Exception ex)
            {
                // throw ex;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
            return View(modelVM);
        }

        // GET: VehicleModels/Edit/5
        public async Task<ActionResult> EditAsync(Guid? id)
        {
            ViewBag.Make = service.PopulateMakesDropDownList();
            return await ViewPageAsync(id);
        }

        // POST: VehicleModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind(Include = "VehicleModelId,VehicleMakeId,Name,Abrv")] Guid id, ModelVM modelVM)
        {
            if (ModelState.IsValid)
            {
                var dest = Mapper.Map<VehicleModel>(modelVM);
                await service.EditAsync(id, dest);

                return RedirectToAction("");
            }
            return View(modelVM);
        }

        // GET: VehicleModels/Delete/5
        public async Task<ActionResult> DeleteAsync(Guid? id)
        {
            return await ViewPageAsync(id);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("DeleteAsync")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync(Guid id)
        {
            try
            {
                if (id != null)
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

            VehicleModel vehicleModel = await service.SelectByIDAsync(id.Value);
            var dest = Mapper.Map<ModelVM>(vehicleModel);
            if (dest == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(dest);
        }
    }
}
