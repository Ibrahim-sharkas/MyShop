using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
     public class BasketItemViewModel
    {
        public string Id { get; set; }// the id from basketitem
        public int Quantity { get; set; }// the quantity from basketitem
        public string ProductName { get; set; }// the product name from product table
        public decimal Price { get; set; }// the price in product table
        public string Img { get; set; }// the image url from product table
    }
}
