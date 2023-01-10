using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resto.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, Enter your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, Enter Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please, Select Number of Guests")]
        public string NumberOfGuests { get; set; }

        [Required(ErrorMessage = "Please, Select Number of Tables")]
        public string NumberOfTables { get; set; }

        [Required(ErrorMessage = "Select Reservation Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Select Reservation Time")]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}