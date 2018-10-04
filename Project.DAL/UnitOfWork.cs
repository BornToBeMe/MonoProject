using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL
{
    public class UnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        private CarContext context = new CarContext();

        /// <summary>
        /// Sets the GenericRepository as MakeRepository
        /// </summary>
        private GenericRepository<VehicleMake> makeRepository;

        /// <summary>
        /// Sets the GenericRepository as ModelRepository
        /// </summary>
        private GenericRepository<VehicleModel> modelRepository;

        /// <summary>
        /// Sets the GenericRepository as MakeRepository
        /// </summary>
        public GenericRepository<VehicleMake> MakeRepository
        {
            get
            {
                if (this.makeRepository == null)
                {
                    this.makeRepository = new GenericRepository<VehicleMake>(context);
                }
                return makeRepository;
            }
        }

        /// <summary>
        /// Sets the GenericRepository as ModelRepository
        /// </summary>
        public GenericRepository<VehicleModel> ModelRepository
        {
            get
            {
                if (this.modelRepository == null)
                {
                    this.modelRepository = new GenericRepository<VehicleModel>(context);
                }
                return modelRepository;
            }
        }

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
