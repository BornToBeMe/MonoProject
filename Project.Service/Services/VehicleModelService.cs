using Project.Service.DAL;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Service.Services
{
    public interface IVehicleModelService
    {
        Task<IPagedList<VehicleModel>> SelectAllAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task<VehicleModel> SelectByIDAsync(int id);
        Task<string> InsertAsync(VehicleModel obj);
        Task<string> UpdateAsync(VehicleModel obj);
        Task<string> DeleteAsync(int id);
    }

    class VehicleModelService : IVehicleModelService
    {
        async Task<IPagedList<VehicleModel>> IVehicleModelService.SelectAllAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            using(var context = new CarContext())
            {
                var query = from c in context.VehicleModels select c;

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

                int pageSize = 3;
                int pageNumber = (page ?? 1);

                IPagedList<VehicleModel> data = await query.ToPagedListAsync(pageNumber, pageSize);
                return data;
            }
        }

        async Task<VehicleModel> IVehicleModelService.SelectByIDAsync(int id)
        {
            using(var context = new CarContext())
            {
                var query = from c in context.VehicleModels where c.VehicleModelID == id select c;
                VehicleModel model = await query.SingleOrDefaultAsync();
                return model;
            }
        }

        async Task<string> IVehicleModelService.InsertAsync(VehicleModel obj)
        {
            using(var context = new CarContext())
            {
                context.VehicleModels.Add(obj);
                await context.SaveChangesAsync();
                return "Model added successfully";
            }
        }

        async Task<string> IVehicleModelService.UpdateAsync(VehicleModel obj)
        {
            using(var context = new CarContext())
            {
                VehicleModel existing = await context.VehicleModels.FindAsync(obj.VehicleModelID);
                existing.Name = obj.Name;
                existing.Abrv = obj.Abrv;
                existing.VehicleMakeID = obj.VehicleMakeID;
                await context.SaveChangesAsync();
                return "Model updated succcessfully";
            }
        }

        async Task<string> IVehicleModelService.DeleteAsync(int id)
        {
            using(var context = new CarContext())
            {
                VehicleModel existing = await context.VehicleModels.FindAsync(id);
                context.VehicleModels.Remove(existing);
                await context.SaveChangesAsync();
                return "Model updated successfully";
            }
        }
    }
}
