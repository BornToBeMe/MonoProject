﻿using AutoMapper;
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
    public class MakeRepository : IMakeRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        private UnitOfWork unitOfWork = new UnitOfWork();

        #endregion

        #region Methods

        /// <summary>
        /// Gets All Vehicle Makes
        /// </summary>
        /// <param name="sortOrder">Sort Order.</param>
        /// <param name="search">Search.</param>
        /// <param name="pagination">Paging.</param>
        /// <returns>Paged List of Vehicle Makes</returns>
        public async Task<IPagedList<IVehicleMake>> SelectAllAsync(ISorting sortBy, ISearch search, IPaging pagination)
        {
            var query = await unitOfWork.MakeRepository.GetAll();

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


            var map = Mapper.Map<IEnumerable<IVehicleMake>>(await query.ToListAsync());
            return map.ToPagedList(pageNumber, pageSize);
        }

        /// <summary>
        /// Gets Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        public async Task<IVehicleMake> SelectByIDAsync(Guid id)
        {
            IVehicleMake make = Mapper.Map<IVehicleMake>(await unitOfWork.MakeRepository.GetByID(id));//await Context.VehicleMakes.Where(c => c.Id == id).SingleOrDefaultAsync());
            return make;
        }

        /// <summary>
        /// Creates a Vehicle Make.
        /// </summary>
        /// <param name="obj">Vehicle Make being Created.</param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(IVehicleMake obj)
        {
            var map = Mapper.Map<VehicleMake>(obj);
            map.Id = Guid.NewGuid();
            await unitOfWork.MakeRepository.Insert(map);
            return (await unitOfWork.Save() > 0);
        }

        /// <summary>
        /// Edits Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="vehicleMake">Vehicle Make being passed in.</param>
        /// <returns></returns>
        public async Task<bool> EditAsync(Guid id, IVehicleMake vehicleMake)
        {
            var updated = Mapper.Map<VehicleMake>(vehicleMake);
            await unitOfWork.MakeRepository.UpdateAsync(updated, id);

            return (await unitOfWork.Save() > 0);
        }

        /// <summary>
        /// Removes Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            VehicleMake existing = await unitOfWork.MakeRepository.GetByID(id);
            await unitOfWork.MakeRepository.DeleteAsync(existing);
            return (await unitOfWork.Save() > 0);
        }
    }

    #endregion
}
