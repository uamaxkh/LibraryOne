using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Models
{
    /// <summary>
    /// Reflects Author from DB
    /// </summary>
    public class Author : BaseEntity
    {
        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Ім'я і прізвище")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Книги")]
        [UIHint("Collection")]
        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }
}
