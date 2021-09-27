using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;



namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product); 

        }
        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            else
            {
                context.Insert(p);
                context.Commit();
                return RedirectToAction("Index");
            }
            
        }
        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if(product != null)
            {
                return View(product);
            }
            else
            {
                 return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult Edit(Product p, string id)
        {
            Product editedProduct = context.Find(id);
            if (p != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(p);
                }
                editedProduct.Catagorey = p.Catagorey;
                editedProduct.Description = p.Description;
                editedProduct.Name = p.Name;
                editedProduct.Price = p.Price;
                editedProduct.Image = p.Image;
                context.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        public ActionResult Delete(string id)
        {
            Product productToDelete = context.Find(id);
            if (productToDelete != null)
            {

                return View(productToDelete);
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product productToDelete = context.Find(id);
            if (productToDelete != null)
            {

                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
            
        }
    }
}