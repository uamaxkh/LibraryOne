using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Models
{
    public class Section
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Назва розділу")]
        public string Name { get; set; }
        
        public ICollection<Book> Books { get; set; }
    }
}
