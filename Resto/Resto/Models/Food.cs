using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resto.Models
{
    public class Food
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Food Name")]
        public string Name { get; set; }

        public string Size { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Invalid Price!")]
        [Required(ErrorMessage = "Please Enter Food Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please Select Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}