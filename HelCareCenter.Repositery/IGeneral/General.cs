using HelCareCenter.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.Repositery.IGeneral
{
    public abstract class General<T> : IGeneral<T> where T : class

    {
        protected readonly DBContext db;


        public General(DBContext _db)
        {
            db = _db;
        }
        public virtual async Task<T> ADD(T addedItem)
        {

            try
            {
                if (addedItem == null)
                    return null;
                await db.AddAsync<T>(addedItem);
                await db.SaveChangesAsync();
                return addedItem;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error When Add Item");
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public virtual async Task<T> Delete(int id)
        {
            T t = await GetById(id);
            if (t == null)
                return null;
            db.Set<T>().Remove(t);
            await db.SaveChangesAsync();
            return t;
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await db.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> Update(T UpdatedItem)
        {
            if (UpdatedItem == null)
                return null;
            db.Set<T>().Update(UpdatedItem);
            await db.SaveChangesAsync();
            return UpdatedItem;
        }
    }
}
