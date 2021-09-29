using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts;
using MyShop.Core.Models;


namespace MyShop.DataAccess.InMemory
{
   public class InMemoryReposetory<T> : IReposetory<T> where T: BaseEntity
    {
        ObjectCache cach = MemoryCache.Default;
        List<T> items;
        string className;
        public InMemoryReposetory()
        {
            className = typeof(T).Name;
            items = cach[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }
        public void Commit()
        {
            cach[className] = items;
        }
        public void Insert(T t)
        {
            items.Add(t);
        }
        public void Update(T t)
        {
            T tToUpdate = items.Find(i => i.Id == t.Id);
            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
        public T Find(string id)
        {
            T tToFound = items.Find(i => i.Id == id);
            if (tToFound != null)
            {
                return tToFound;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
        public void Delete(string id)
        {
            T tToDelet = items.Find(i=>i.Id==id);
            if (tToDelet != null)
            {
                items.Remove(tToDelet);
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
    }
}
