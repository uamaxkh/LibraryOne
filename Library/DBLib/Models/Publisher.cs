using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Models
{
    /// <summary>
    /// Reflects Publisher from DB
    /// </summary>
    public class Publisher : BaseEntity
    {
        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Назва видавця")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Місто")]
        public string City { get; set; }

        public ICollection<Book> Books { get; set; }

        public Publisher()
        {
            Books = new List<Book>();
        }
    }
}
