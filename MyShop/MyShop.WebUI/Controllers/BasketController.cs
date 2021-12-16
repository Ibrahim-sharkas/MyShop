using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IReposetory<Customer> customers;
        IBasketService basketService;
        IOrderService orderService;
        public BasketController(IBasketService BasketService,IOrderService OrderService,IReposetory<Customer> Customers)
        {
            this.basketService = BasketService;
            this.orderService = OrderService;
            this.customers = Customers;
        }
        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }
        public ActionResult AddToBasket(string id)
        {
            basketService.AddToBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromBasket( string id)
        {
            basketService.RemoveFromBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }
        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }
        [Authorize]

        public ActionResult CheckOut()
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if (customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    lastName = customer.LastName,
                    Adress = customer.Address
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
           
        }
        [HttpPost]
        [Authorize]
        public ActionResult CheckOut( Order order)
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.Email = User.Identity.Name; 
           // order.OrderStauts = "Order Created";
            // payment proccess
           // order.OrderStauts = "Paid Order";
            orderService.CreateOrder(order, basketItems);
            basketService.Clearbasket(this.HttpContext);

           return RedirectToAction ("ThankYou" , new { OrderId = order.Id });
        }
         public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}