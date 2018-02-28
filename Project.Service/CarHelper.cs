using Project.Service.Models;
using Project.Service.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Project.Service
{
    public class CarHelper
    {
        public static async Task<List<Car>> SelectAllAsync()
        {
            CarContext db = new CarContext();
            var query = from c in db.Cars orderby c.CarID ascending select c;
            return await query.ToListAsync();
        }

        public static async Task<Car> SelectByIDAsync(int id)
        {
            CarContext db = new CarContext();
            var query = from c in db.Cars where c.CarID == id select c;
            Car obj = await query.SingleOrDefaultAsync();
            return obj;
        }

        public static async Task<string> InsertAsync(Car obj)
        {
            CarContext db = new CarContext();
            db.Cars.Add(obj);
            await db.SaveChangesAsync();
            return "Car added successfully!";
        }

        public static async Task<string> UpdateAsync(Car obj)
        {
            CarContext db = new CarContext();
            Car existing = await db.Cars.FindAsync(obj.CarID);
            existing.VehicleMake = obj.VehicleMake;
            existing.VehicleModel = obj.VehicleModel;
            await db.SaveChangesAsync();
            return "Car updated successfully!";
        }

        public static async Task<string> DeleteAsync(int id)
        {
            CarContext db = new CarContext();
            Car existing = await db.Cars.FindAsync(id);
            db.Cars.Remove(existing);
            await db.SaveChangesAsync();
            return "Car deleted successfully!";
        }
    }
}
