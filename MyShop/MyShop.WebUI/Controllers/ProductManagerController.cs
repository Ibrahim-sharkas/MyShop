using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.Core.Contracts;
using System.IO;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IReposetory<Product> context;
        IReposetory<ProductCatagory> productCatgories;
        public ProductManagerController(IReposetory<Product> productContext, IReposetory<ProductCatagory> productCatgories)
        {
            
            context = productContext;
           this.productCatgories = productCatgories;
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
        public ActionResult Create(ProductManagerViewModel p,HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }
            else
            {
                if (file != null)
                {
                    p.product.Image = p.product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//")+p.product.Image);
                }
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
        public ActionResult Edit(ProductManagerViewModel p,HttpPostedFileBase file)
        {
            Product editedProduct = context.Find(p.product.Id);
            if (editedProduct != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(p);
                }
                if (file != null)
                {
                    editedProduct.Image = p.product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + editedProduct.Image);
                }
                editedProduct.Catagorey = p.product.Catagorey;
                editedProduct.Description = p.product.Description;
                editedProduct.Name = p.product.Name;
                editedProduct.Price = p.product.Price;
                



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