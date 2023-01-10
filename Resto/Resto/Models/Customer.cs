using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resto.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }

    public class CustomerLogin
    {
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}