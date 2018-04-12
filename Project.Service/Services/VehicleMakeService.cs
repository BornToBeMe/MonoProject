﻿using AutoMapper;
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
        public async Task<IPagedList<VehicleMake>> SelectAllAsync(ISorting sortOrder, ISearch search, IPaging pagination)
        {
            using (var context = new CarContext())
            {
                var query = context.VehicleMakes.AsQueryable();

                if (!String.IsNullOrEmpty(search.CurrentFilter))
                {
                    pagination.PageNumber = 1;
                    query = query.Where(q => q.Name.Contains(search.CurrentFilter) || q.Abrv.Contains(search.CurrentFilter));
                }

                switch (sortOrder.SortOrder)
                {
                    case "Name_desc":
                        query = query.OrderByDescending(q => q.Name);
                        break;
                    case "Abrv":
                        query = query.OrderBy(q => q.Abrv);
                        break;
                    case "Abrv_desc":
                        query = query.OrderByDescending(q => q.Abrv);
                        break;
                    default:
                        query = query.OrderBy(q => q.Name);
                        break;
                }

                int pageSize = (pagination.PageSize ?? 3);
                int pageNumber = (pagination.PageNumber ?? 1);

                IPagedList<VehicleMake> data = await query.ToPagedListAsync(pageNumber, pageSize);
                return data;
            }
        }

        public async Task<VehicleMake> SelectByIDAsync(Guid id)
        {
            using (var context = new CarContext())
            {
                VehicleMake make = await context.VehicleMakes.Where(c => c.Id == id).SingleOrDefaultAsync();
                return make;
            }
        }

        public async Task<bool> CreateAsync(VehicleMake obj)
        {
            using (var context = new CarContext())
            {
                obj.Id = Guid.NewGuid();
                context.VehicleMakes.Add(obj);
                await context.SaveChangesAsync();
                return (await context.SaveChangesAsync() > 0);
            }
        }

        public async Task<VehicleMake> EditAsync(Guid id, VehicleMake vehicleMake)
        {
            using (var context = new CarContext())
            {
                var entity = await context.VehicleMakes.FindAsync(id);
                if(entity == null)
                {
                    throw new ArgumentNullException();
                }
                context.Entry(entity).CurrentValues.SetValues(vehicleMake);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var context = new CarContext())
            {
                VehicleMake existing = await context.VehicleMakes.FindAsync(id);
                context.VehicleMakes.Remove(existing);
                await context.SaveChangesAsync();
                return (await context.SaveChangesAsync() > 0);
            }

        }
    }
}
