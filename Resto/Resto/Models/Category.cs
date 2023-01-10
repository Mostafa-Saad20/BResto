using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resto.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, Enter Category Name")]
        public string Name { get; set; }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Please, Enter Category Image")]
        public string Image { get; set; }

        public ICollection<Food> Foods { get; set; }
    }
}