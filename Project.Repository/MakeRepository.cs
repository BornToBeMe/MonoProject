using AutoMapper;
using Project.Common;
using Project.DAL;
using Project.Model.Common;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Repository
{
    public class MakeRepository : IMakeRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes an instance of the <see cref="GenericRepository{VehicleMake}" /> class.
        /// </summary>
        private readonly IGenericRepository<VehicleMake> makeRepository;

        IUnitOfWorkFactory unitOfWorkFactory { get; set; }

        public MakeRepository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            makeRepository = new GenericRepository<VehicleMake>(unitOfWorkFactory);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets All Vehicle Makes
        /// </summary>
        /// <param name="sortOrder">Sort Order.</param>
        /// <param name="search">Search.</param>
        /// <param name="pagination">Paging.</param>
        /// <returns>Paged List of Vehicle Makes</returns>
        public async Task<IPagedList<IVehicleMake>> SelectAsync(ISorting sortBy, ISearch search, IPaging pagination)
        {
            Expression<Func<VehicleMake, bool>> filter = null;
            if (!String.IsNullOrEmpty(search.CurrentFilter))
            {
                filter = q => q.Name.Contains(search.CurrentFilter) || q.Abrv.Contains(search.CurrentFilter);
            }

            var query = await makeRepository.SelectAsync(
                filter: filter, 
                orderBy: q => {
                    if (sortBy.SortBy == "Name")
                    {
                        if (sortBy.SortAscending)
                        {
                            return q.OrderBy(i => i.Name);
                        }
                        else
                        {
                            return q.OrderByDescending(i => i.Name);
                        }
                    }
                    else if (sortBy.SortBy == "Abrv")
                    {
                        if (sortBy.SortAscending)
                        {
                            return q.OrderBy(i => i.Abrv);
                        }
                        else
                        {
                            return q.OrderByDescending(i => i.Abrv);
                        }
                    }
                    else
                    {
                        return q.OrderBy(i => i.Name);
                    }
                },
                includeProperties: null,
                paged: pagination);

            var map = Mapper.Map<IEnumerable<IVehicleMake>>(query);
            return new StaticPagedList<IVehicleMake>(map, query.GetMetaData());

        }

        /// <summary>
        /// Gets Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        public async Task<IVehicleMake> SelectByIDAsync(Guid id)
        {
            IVehicleMake make = Mapper.Map<IVehicleMake>(await makeRepository.SelectByIDAsync(id));
            return make;
        }

        /// <summary>
        /// Creates a Vehicle Make.
        /// </summary>
        /// <param name="obj">Vehicle Make being Created.</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(IVehicleMake obj)
        {
            var map = Mapper.Map<VehicleMake>(obj);
            map.Id = Guid.NewGuid();
            return await makeRepository.InsertAsync(map);
        }

        /// <summary>
        /// Edits Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <param name="vehicleMake">Vehicle Make being passed in.</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Guid id, IVehicleMake vehicleMake)
        {
            var updated = Mapper.Map<VehicleMake>(vehicleMake);
            return await makeRepository.UpdateAsync(updated, id);
        }

        /// <summary>
        /// Removes Vehicle Make with specific Id.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            VehicleMake existing = await makeRepository.SelectByIDAsync(id);
            return await makeRepository.DeleteAsync(existing);
        }
    }

    #endregion
}
