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
    public class VehicleMakeService : IVehicleMakeService
    {
        async Task<IPagedList<VehicleMake>> IVehicleMakeService.SelectAllAsync(Sorting sortOrder, string currentFilter, string searchString, Paging pagination)
        {
            using (var context = new CarContext())
            {
                var query = from c in context.VehicleMakes select c;

                if (searchString != null)
                {
                    pagination.PageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    query = query.Where(q => q.Name.Contains(searchString) || q.Abrv.Contains(searchString));
                }

                pagination.PageSize = 3;
                int pageNumber = (pagination.PageNumber ?? 1);

                IPagedList<VehicleMake> data = await query.OrderBy(Sorting => sortOrder.SortOrder).ToPagedListAsync(pageNumber, pagination.PageSize);
                return data;
            }
        }

        async Task<VehicleMake> IVehicleMakeService.SelectByIDAsync(Guid id)
        {
            using (var context = new CarContext())
            {
                VehicleMake make = await context.VehicleMakes.Where(c => c.ID == id).SingleOrDefaultAsync();
                return make;
            }

        }

        async Task<bool> IVehicleMakeService.InsertAsync(VehicleMake obj)
        {
            using (var context = new CarContext())
            {
                context.VehicleMakes.Add(obj);
                await context.SaveChangesAsync();
                bool added;
                return added = true;
            }

        }

        async Task<VehicleMake> IVehicleMakeService.UpdateAsync(Guid id, VehicleMake vehicleMake)
        {
            using (var context = new CarContext())
            {
                var entity = await context.VehicleMakes.FindAsync(id);
                context.Entry(entity).CurrentValues.SetValues(vehicleMake);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        async Task<bool> IVehicleMakeService.DeleteAsync(Guid id)
        {
            using (var context = new CarContext())
            {
                VehicleMake existing = await context.VehicleMakes.FindAsync(id);
                context.VehicleMakes.Remove(existing);
                await context.SaveChangesAsync();
                bool deleted;
                return deleted = true;
            }

        }
    }
}
