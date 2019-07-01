using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Models
{
    /// <summary>
    /// Reflects section from DB
    /// </summary>
    public class Section : BaseEntity
    {
        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Назва розділу")]
        public string Name { get; set; }
        
        public ICollection<Book> Books { get; set; }

        public Section()
        {
            Books = new List<Book>();
        }
    }
}
