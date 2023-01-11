using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resto.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please, Enter your Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, Enter your Message")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}