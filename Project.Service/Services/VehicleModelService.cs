﻿using Project.Service.DAL;
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
        async Task<IPagedList<VehicleModel>> IVehicleModelService.SelectAllAsync(ISorting sorting, IFilter filter, ISearch search, IPaging pagination)
        {
            using(var context = new CarContext())
            {
                var query = from c in context.VehicleModels select c;
                ISorting sortOrder = new Sorting();
                IFilter currentFilter = new Filter();
                ISearch searchString = new Search();
                IPaging paging = new Paging();

                if(searchString.SearchString != null)
                {
                    paging.PageNumber = 1;
                }
                else
                {
                    searchString.SearchString = currentFilter.CurrentFilter;
                }

                if (!String.IsNullOrEmpty(searchString.SearchString))
                {
                    query = query.Where(q => q.Name.Contains(searchString.SearchString) || q.Abrv.Contains(searchString.SearchString));
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

                paging.PageSize = 3;
                int pageNumber = (paging.PageNumber ?? 1);

                IPagedList<VehicleModel> data = await query.ToPagedListAsync(pageNumber, paging.PageSize);
                return data;
            }
        }

        async Task<VehicleModel> IVehicleModelService.SelectByIDAsync(Guid id)
        {
            using(var context = new CarContext())
            {
                VehicleModel model = await context.VehicleModels.Where(c => c.VehicleModelId == id).SingleOrDefaultAsync();
                return model;
            }
        }

        async Task<bool> IVehicleModelService.InsertAsync(VehicleModel obj)
        {
            using(var context = new CarContext())
            {
                context.VehicleModels.Add(obj);
                await context.SaveChangesAsync();
                bool added = true;
                return added;
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

        async Task<bool> IVehicleModelService.DeleteAsync(Guid id)
        {
            using(var context = new CarContext())
            {
                VehicleModel existing = await context.VehicleModels.FindAsync(id);
                context.VehicleModels.Remove(existing);
                await context.SaveChangesAsync();
                bool deleted = true;
                return deleted;
            }
        }
    }
}
