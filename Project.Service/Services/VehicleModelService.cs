using AutoMapper;
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
        public async Task<IPagedList<VehicleModel>> SelectAllAsync(ISorting sorting, ISearch search, IPaging pagination)
        {
            using(var context = new CarContext())
            {
                var query = context.VehicleModels.Include(s => s.VehicleMake).AsQueryable();

                if (!String.IsNullOrEmpty(search.CurrentFilter))
                {
                    pagination.PageNumber = 1;
                    query = query.Where(q => q.Name.Contains(search.CurrentFilter) || q.Abrv.Contains(search.CurrentFilter) || q.VehicleMake.Name.Contains(search.CurrentFilter));
                }

                switch (sorting.SortOrder)
                {
                    case "make_desc":
                        query = query.OrderByDescending(q => q.VehicleMake.Name);
                        break;
                    case "Name":
                        query = query.OrderBy(q => q.Name);
                        break;
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
                        query = query.OrderBy(q => q.VehicleMake.Name);
                        break;
                }

                pagination.PageSize = 3;
                int pageNumber = (pagination.PageNumber ?? 1);

                IPagedList<VehicleModel> data = await query.ToPagedListAsync(pageNumber, pagination.PageSize);
                return data;
            }
        }

        public async Task<VehicleModel> SelectByIDAsync(Guid id)
        {
            using(var context = new CarContext())
            {
                VehicleModel model = await context.VehicleModels.Where(c => c.VehicleModelId == id).SingleOrDefaultAsync();
                return model;
            }
        }

        public async Task<bool> InsertAsync(VehicleModel obj)
        {
            using(var context = new CarContext())
            {
                obj.VehicleModelId = Guid.NewGuid();
                context.VehicleModels.Add(obj);
                await context.SaveChangesAsync();
                return (await context.SaveChangesAsync() > 0);
            }
        }

        public async Task<VehicleModel> UpdateAsync(Guid id, VehicleModel vehicleModel)
        {
            using(var context = new CarContext())
            {
                var entity = await context.VehicleModels.FindAsync(id);
                context.Entry(entity).CurrentValues.SetValues(vehicleModel);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using(var context = new CarContext())
            {
                VehicleModel existing = await context.VehicleModels.FindAsync(id);
                context.VehicleModels.Remove(existing);
                await context.SaveChangesAsync();
                return (await context.SaveChangesAsync() > 0);
            }
        }

        public IList<VehicleMake> PopulateMakesDropDownList()
        {
            using (var context = new CarContext())
            {
                List<VehicleMake> makes = context.VehicleMakes.OrderBy(c => c.Name).ToList();
                return makes;
            }

        }
    }
}
