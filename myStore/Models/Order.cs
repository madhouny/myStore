using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myStore.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string UserID { get; set; }
       public DateTime OrderAt { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}