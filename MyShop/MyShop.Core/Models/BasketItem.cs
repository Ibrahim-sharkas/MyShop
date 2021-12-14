using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
   public class BasketItem :BaseEntity
    {
        public string BasketId { get; set; }// link to the basket id
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
