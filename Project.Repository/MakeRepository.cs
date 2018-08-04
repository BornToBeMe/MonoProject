using AutoMapper;
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
    public class MakeRepository : IMakeRepository
    {
        public MakeRepository(ICarContext context)
        {
            this.Context = context;
        }

        protected ICarContext Context { get; private set; }

        public async Task<IPagedList<IVehicleMake>> SelectAllAsync(ISorting sortBy, ISearch search, IPaging pagination)
        {
            var query = Context.VehicleMakes.AsQueryable();

            if (!String.IsNullOrEmpty(search.CurrentFilter))
            {
                query = query.Where(q => q.Name.Contains(search.CurrentFilter) || q.Abrv.Contains(search.CurrentFilter));
            }

            if (sortBy.SortBy == "Name")
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

            var map = Mapper.Map<IPagedList<IVehicleMake>>(query);
            IPagedList<IVehicleMake> data = await map.ToPagedListAsync(pageNumber, pageSize);
            return data;
        }

        public async Task<IVehicleMake> SelectByIDAsync(Guid id)
        {
            IVehicleMake make = Mapper.Map<IVehicleMake>(await Context.VehicleMakes.Where(c => c.Id == id).SingleOrDefaultAsync());
            return make;
        }

        public async Task<bool> CreateAsync(Project.Model.VehicleMake obj)
        {
            var map = Mapper.Map<VehicleMake>(obj);
            map.Id = Guid.NewGuid();
            Context.VehicleMakes.Add(map);
            return (await Context.SaveChangesAsync() > 0);
        }

        public async Task<bool> EditAsync(Guid id, Project.Model.VehicleMake vehicleMake)
        {
            var entity = await Context.VehicleMakes.FindAsync(id);
            var map = Mapper.Map<Project.Model.VehicleMake>(entity);
            if (map == null)
            {
                throw new ArgumentNullException();
            }
            Context.Entry(entity).CurrentValues.SetValues(vehicleMake);

            return (await Context.SaveChangesAsync() > 0);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            VehicleMake existing = await Context.VehicleMakes.FindAsync(id);
            Context.VehicleMakes.Remove(existing);
            return (await Context.SaveChangesAsync() > 0);
        }
    }
}
