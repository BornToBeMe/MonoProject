using Project.Service.DAL;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Services
{
    public interface IVehicleMakeService
    {
        Task<IPagedList<VehicleMake>> SelectAllAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task<VehicleMake> SelectByIDAsync(int id);
        Task<string> InsertAsync(VehicleMake obj);
        Task<string> UpdateAsync(VehicleMake obj);
        Task<string> DeleteAsync(int id);
    }

    public class VehicleMakeService : IVehicleMakeService
    {
        async Task<IPagedList<VehicleMake>> IVehicleMakeService.SelectAllAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            using (var context = new CarContext())
            {
                dynamic ViewBag = new System.Dynamic.ExpandoObject();
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.AbrvSortParm = sortOrder == "Abrv" ? "abrv_desc" : "Abrv";
                var query = from c in context.VehicleMakes select c;

                if(searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(q => q.Name.Contains(searchString) || q.Abrv.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        query = query.OrderByDescending(q => q.Name);
                        break;
                    case "Abrv":
                        query = query.OrderBy(q => q.Abrv);
                        break;
                    case "abrv_desc":
                        query = query.OrderByDescending(q => q.Abrv);
                        break;
                    default:
                        query = query.OrderBy(q => q.Name);
                        break;
                }

                int pageSize = 3;
                int pageNumber = (page ?? 1);


                IPagedList<VehicleMake> data = await query.ToPagedListAsync(pageNumber, pageSize);
                return data;
            }
        }

        async Task<VehicleMake> IVehicleMakeService.SelectByIDAsync(int id)
        {
            using (var context = new CarContext())
            {
                var query = from c in context.VehicleMakes where c.ID == id select c;
                VehicleMake make = await query.SingleOrDefaultAsync();
                return make;
            }

        }

        async Task<string> IVehicleMakeService.InsertAsync(VehicleMake obj)
        {
            using (var context = new CarContext())
            {
                context.VehicleMakes.Add(obj);
                await context.SaveChangesAsync();
                return "Make added successfully!";
            }

        }

        async Task<string> IVehicleMakeService.UpdateAsync(VehicleMake obj)
        {
            using (var context = new CarContext())
            {
                VehicleMake existing = await context.VehicleMakes.FindAsync(obj.ID);
                existing.Name = obj.Name;
                existing.Abrv = obj.Abrv;
                await context.SaveChangesAsync();
                return "Make updated successfully!";
            }

        }

        async Task<string> IVehicleMakeService.DeleteAsync(int id)
        {
            using (var context = new CarContext())
            {
                VehicleMake existing = await context.VehicleMakes.FindAsync(id);
                context.VehicleMakes.Remove(existing);
                await context.SaveChangesAsync();
                return "Make deleted successfully!";
            }

        }
    }
}
