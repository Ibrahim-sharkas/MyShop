using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;

namespace MyShop.WebUI.Controllers
{
    public class ProductCatagoryController : Controller
    {
       InMemoryReposetory< ProductCatagory> context;
        public ProductCatagoryController()
        {
            context = new InMemoryReposetory<ProductCatagory>();
        }
        // GET: ProductCatagory
        public ActionResult Index()
        {
            List<ProductCatagory> productCatagories= context.Collection().ToList();
            return View(productCatagories);
        }
        public ActionResult Add()
        {
            ProductCatagory cat = new ProductCatagory();
            return View(cat);
            
        }
        [HttpPost]
        public ActionResult Add( ProductCatagory productCatagory)
        {
            context.Insert(productCatagory);
            context.Commit();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string id)
        {
            ProductCatagory cat = context.Find(id);
            if (cat != null)
            {
                return View(cat);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCatagory cat,string id)
        {
            ProductCatagory productCatagoryEdit = context.Find(id);
            if (productCatagoryEdit != null)
            {
                productCatagoryEdit.Catagory = cat.Catagory;
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
            ProductCatagory catToDelete = context.Find(id);
            if (catToDelete != null)
            {
                return View(catToDelete);
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfrimDelete(string id)
        {
            ProductCatagory catTodelete = context.Find(id);
            if (catTodelete != null)
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