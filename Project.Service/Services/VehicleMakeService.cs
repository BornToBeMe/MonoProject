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
        async Task<IPagedList<VehicleMake>> IVehicleMakeService.SelectAllAsync(ISorting sortOrder, ISearch search, IPaging pagination)
        {
            using (var context = new CarContext())
            {
                var query = from c in context.VehicleMakes select c;

                if (search.SearchString != null)
                {
                    pagination.PageNumber = 1;
                }
                else
                {
                    search.SearchString = search.CurrentFilter;
                }

                if (!String.IsNullOrEmpty(search.SearchString))
                {
                    query = query.Where(q => q.Name.Contains(search.SearchString) || q.Abrv.Contains(search.SearchString));
                }

                switch (sortOrder.SortOrder)
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

                pagination.PageSize = 3;
                int pageNumber = (pagination.PageNumber ?? 1);

                IPagedList<VehicleMake> data = await query.ToPagedListAsync(pageNumber, pagination.PageSize);
                return data;
            }
        }

        async Task<VehicleMake> IVehicleMakeService.SelectByIDAsync(Guid id)
        {
            using (var context = new CarContext())
            {
                VehicleMake make = await context.VehicleMakes.Where(c => c.Id == id).SingleOrDefaultAsync();
                return make;
            }

        }

        async Task<bool> IVehicleMakeService.InsertAsync(VehicleMake obj)
        {
            using (var context = new CarContext())
            {
                obj.Id = Guid.NewGuid();
                context.VehicleMakes.Add(obj);
                await context.SaveChangesAsync();
                bool added = true;
                return added;
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
                bool deleted = true;
                return deleted;
            }

        }
    }
}
