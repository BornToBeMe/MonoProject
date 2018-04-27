using AutoMapper;
using Project.Service.DAL;
using Project.Service;
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
        public async Task<IPagedList<VehicleModel>> SelectAllAsync(ISorting sortBy, ISearch search, IPaging pagination)
        {
            using (var context = new CarContext())
            {
                var query = context.VehicleModels.Include(s => s.VehicleMake).AsQueryable();

                if (!String.IsNullOrEmpty(search.CurrentFilter))
                {
                    query = query.Where(q => q.Name.Contains(search.CurrentFilter) || q.Abrv.Contains(search.CurrentFilter) || q.VehicleMake.Name.Contains(search.CurrentFilter));
                }

                if (sortBy.SortBy == "Make")
                {
                    if (sortBy.SortAscending)
                    {
                        query = query.OrderBy(q => q.VehicleMake.Name);
                    }
                    else
                    {
                        query = query.OrderByDescending(q => q.VehicleMake.Name);
                    }
                }
                else if (sortBy.SortBy == "Name")
                {
                    if (sortBy.SortAscending)
                    {
                        query = query.OrderBy(q => q.Name);
                    }
                    else
                    {
                        query = query.OrderByDescending(q => q.Name);
                    }
                }
                else if (sortBy.SortBy == "Abrv")
                {
                    if (sortBy.SortAscending)
                    {
                        query = query.OrderBy(q => q.Abrv);
                    }
                    else
                    {
                        query = query.OrderByDescending(q => q.Abrv);
                    }
                }
                else
                {
                    query = query.OrderBy(q => q.Name);
                }

                int pageSize = (pagination.PageSize ?? 3);
                int pageNumber = (pagination.PageNumber ?? 1);

                IPagedList<VehicleModel> data = await query.ToPagedListAsync(pageNumber, pageSize);
                return data;
            }
        }

        public async Task<VehicleModel> SelectByIDAsync(Guid id)
        {
            using (var context = new CarContext())
            {
                VehicleModel model = await context.VehicleModels.Where(c => c.VehicleModelId == id).SingleOrDefaultAsync();
                return model;
            }
        }

        public async Task<bool> CreateAsync(VehicleModel obj)
        {
            using (var context = new CarContext())
            {
                obj.VehicleModelId = Guid.NewGuid();
                context.VehicleModels.Add(obj);
                return (await context.SaveChangesAsync() > 0);
            }
        }

        public async Task<VehicleModel> EditAsync(Guid id, VehicleModel vehicleModel)
        {
            using (var context = new CarContext())
            {
                var entity = await context.VehicleModels.FindAsync(id);
                if(entity == null)
                {
                    throw new ArgumentNullException();
                }
                context.Entry(entity).CurrentValues.SetValues(vehicleModel);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var context = new CarContext())
            {
                VehicleModel existing = await context.VehicleModels.FindAsync(id);
                context.VehicleModels.Remove(existing);
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
