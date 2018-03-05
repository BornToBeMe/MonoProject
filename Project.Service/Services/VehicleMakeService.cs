﻿using Project.Service.DAL;
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
        async Task<IPagedList<VehicleMake>> IVehicleMakeService.SelectAllAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            using (var context = new CarContext())
            {
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

        async Task<VehicleMake> IVehicleMakeService.SelectByIDAsync(Guid id)
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

        async Task<string> IVehicleMakeService.DeleteAsync(Guid id)
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
