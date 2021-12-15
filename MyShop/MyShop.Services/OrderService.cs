using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class OrderService : IOrderService
    {
        IReposetory<Order> orderContext;
        public OrderService(IReposetory<Order> OrderContext)
        {
            this.orderContext = OrderContext;
        }
        public void CreateOrder(Order baseorder, List<BasketItemViewModel> basketItems)
        {
            foreach( var item in basketItems)
            {
                baseorder.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.Id,
                    Image = item.Img,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Productname = item.ProductName
                });
            }
            orderContext.Insert(baseorder);
            orderContext.Commit();
        }
    }
}
