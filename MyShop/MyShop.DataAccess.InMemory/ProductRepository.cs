﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        public ProductRepository()
        {
            products = cache["product"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["product"] = products;
        }
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception(" product not found");
            }
        }
        public Product Find(string id)
        {
            Product produtFound = products.Find(p => p.Id == id);
            if(produtFound!= null)
            {
                return produtFound;
            }
            else
            {
                throw new Exception("proc=duct not found");
            }
        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(string id)
        {
            Product deletedProduct = products.Find(p => p.Id == id);
            if (deletedProduct != null)
            {
                products.Remove(deletedProduct);
            }
            else
            {
                throw new Exception("product is not to be found");
            }
        }
    }
}
