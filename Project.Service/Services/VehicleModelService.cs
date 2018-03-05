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
    public class VehicleModelService : IVehicleModelService
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

                IPagedList<VehicleModel> data = await query.ToPagedListAsync(pageNumber, pageSize);
                return data;
            }
        }

        async Task<VehicleModel> IVehicleModelService.SelectByIDAsync(Guid id)
        {
            using(var context = new CarContext())
            {
                var query = from c in context.VehicleModels where c.VehicleModelId == id select c;
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

        async Task<VehicleModel> IVehicleModelService.UpdateAsync(Guid id, VehicleModel vehicleModel)
        {
            using(var context = new CarContext())
            {
                var entity = await context.VehicleModels.FindAsync(id);
                context.Entry(entity).CurrentValues.SetValues(vehicleModel);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        async Task<string> IVehicleModelService.DeleteAsync(Guid id)
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
