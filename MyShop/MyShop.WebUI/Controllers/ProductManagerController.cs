using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;



namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryReposetory<Product> context;
        InMemoryReposetory<ProductCatagory> productCatgories;
        public ProductManagerController()
        {
            
            context = new InMemoryReposetory<Product>();
            productCatgories = new InMemoryReposetory<ProductCatagory>();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.product = new Product();
            viewModel.ProductCatagories = productCatgories.Collection();
            return View(viewModel); 

        }
        [HttpPost]
        public ActionResult Create(ProductManagerViewModel p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            else
            {
                context.Insert(p.product);
                context.Commit();
                return RedirectToAction("Index");
            }
            
        }
        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if(product != null)
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.product = product;
                viewModel.ProductCatagories = productCatgories.Collection();
                return View(viewModel);
            }
            else
            {
                 return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductManagerViewModel p, string id)
        {
            Product editedProduct = context.Find(id);
            if (p != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(p);
                }
                editedProduct.Catagorey = p.product.Catagorey;
                editedProduct.Description = p.product.Description;
                editedProduct.Name = p.product.Name;
                editedProduct.Price = p.product.Price;
                editedProduct.Image = p.product.Image;
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