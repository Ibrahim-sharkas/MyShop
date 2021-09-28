using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
   public class ProductCatagoryRepostory
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCatagory> catagories;
        public ProductCatagoryRepostory()
        {
            catagories = cache[" catagories"] as List<ProductCatagory>;
            if (catagories == null)
            {
                catagories = new List<ProductCatagory>();
            }
        }
        public void Commit()
        {
            cache[" catagories"] = catagories;
        }
        public void Insert(ProductCatagory productCatagory)
        {
            catagories.Add(productCatagory);
        }
        public ProductCatagory Find(string id)
        {
            ProductCatagory catagory = catagories.Find(c => c.Id == id);
            if (catagory == null)
            {
                throw new Exception("catagory not found");
            }
            else
            {
                return catagory;
            }

        }
        public void Delete(string id)
        {
            ProductCatagory DeletCatagory = catagories.Find(c => c.Id == id);
            if (DeletCatagory == null)
            {
                throw new Exception("catgorey not found");
            }
            else
            {
                catagories.Remove(DeletCatagory);
            }
        }
        public void UpdateCatagory(ProductCatagory catagory)
        {
            ProductCatagory CatgoryToUpdate = catagories.Find(c => c.Id ==catagory.Id );
            if (CatgoryToUpdate == null)
            {
                throw new Exception("catagory not found");
            }
            else
            {
                CatgoryToUpdate = catagory;
            }
        }
        public IQueryable<ProductCatagory> CatgoryCollection()
        {
            return catagories.AsQueryable();
        }
    }
}
