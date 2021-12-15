using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Order: BaseEntity
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }
        public string FirstName { get; set; }
        public string lastName { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
      //  public string OrderStauts { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
