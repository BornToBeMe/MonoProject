using AutoMapper;
using Project.Common;
using Project.DAL;
using Project.Model.Common;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Repository
{
    public class ModelRepository : IModelRepository
    {
        public ModelRepository(ICarContext context)
        {
            this.Context = context;
        }

        protected ICarContext Context { get; private set; }

        public async Task<IPagedList<IVehicleModel>> SelectAllAsync(ISorting sortBy, ISearch search, IPaging pagination)
        {
            var query = Context.VehicleModels.Include(s => s.VehicleMake).AsQueryable();

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

            var map = Mapper.Map<IEnumerable<IVehicleModel>>(await query.ToListAsync());
            return map.ToPagedList(pageNumber, pageSize);

        }

        public async Task<IVehicleModel> SelectByIDAsync(Guid id)
        {
            IVehicleModel make = Mapper.Map<IVehicleModel>(await Context.VehicleModels.Where(c => c.VehicleModelId == id).SingleOrDefaultAsync());
            return make;
        }

        public async Task<bool> CreateAsync(Project.Model.VehicleModel obj)
        {
            var map = Mapper.Map<VehicleModel>(obj);
            map.VehicleModelId = Guid.NewGuid();
            Context.VehicleModels.Add(map);
            return (await Context.SaveChangesAsync() > 0);
        }

        public async Task<bool> EditAsync(Guid id, Project.Model.VehicleModel vehicleModel)
        {
            var entity = await Context.VehicleModels.FindAsync(id);
            var map = Mapper.Map<Project.Model.VehicleModel>(entity);
            vehicleModel.VehicleModelId = map.VehicleModelId;
            if (map == null)
            {
                throw new ArgumentNullException();
            }
            Context.Entry(entity).CurrentValues.SetValues(vehicleModel);

            return (await Context.SaveChangesAsync() > 0);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            VehicleModel existing = await Context.VehicleModels.FindAsync(id);
            Context.VehicleModels.Remove(existing);
            return (await Context.SaveChangesAsync() > 0);
        }

        public IList<Project.Model.VehicleMake> PopulateMakesDropDownList()
        {
            List<Project.Model.VehicleMake> makes = Mapper.Map<List<Project.Model.VehicleMake>>(Context.VehicleMakes.OrderBy(c => c.Name).ToList());
            return makes;
        }
    }
}
