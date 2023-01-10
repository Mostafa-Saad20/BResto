using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resto.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string CustomerPhone { get; set; }

        [DataType(DataType.MultilineText)]
        public string DeliveryAddress { get; set; } = "no address";

        [Required(ErrorMessage = "Please select order type")]
        public string Type { get; set; }

        public decimal TotalPrice { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }

}