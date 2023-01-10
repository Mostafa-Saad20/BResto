using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resto.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? FoodId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}