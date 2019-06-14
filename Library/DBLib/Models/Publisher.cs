using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Models
{
    public class Publisher
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Назва видавця")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Місто")]
        public string City { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
