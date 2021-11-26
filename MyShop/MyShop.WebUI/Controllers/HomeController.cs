using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IReposetory<Product> context;
        IReposetory<ProductCatagory> catContext;
        public HomeController(IReposetory<Product> contextProduct,IReposetory<ProductCatagory> contextCat)
        {
            this.context = contextProduct;
            this.catContext = contextCat;
        }

        public HomeController()
        {
        }

        public ActionResult Index(string catagory=null)
        {
            List<Product> products ;
            List<ProductCatagory> catagories = catContext.Collection().ToList();
            if(catagory== null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Catagorey == catagory).ToList();
            }
            ListViewModel model = new ListViewModel();
            model.Products = products;
            model.ProductCatagories = catagories;

            return View(model);
        }
        public ActionResult Details(string id)
        {
            Product p = context.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(p);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}