using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLib.Models;
using System.Web;

namespace Library.Models
{
    public class AddBooksViewModels
    {
        [Required]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Рік видання")]
        public short Year { get; set; }

        [Display(Name = "Читання в бібліотеці")]
        [UIHint("Boolean")]
        public bool LibraryReading { get; set; }

        [Required]
        [Display(Name = "Кількість книг")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Сторінок")]
        public int Pages { get; set; }

        [Required]
        [Display(Name = "Код ISBN")]
        public string ISBN { get; set; }

        [Required]
        [Display(Name = "Назва розділу")]
        public string SectionName { get; set; }

        [Required]
        [Display(Name = "Видавець")]
        public string PublisherName { get; set; }

        [Required]
        [Display(Name = "Автори")]
        public virtual ICollection<Guid> AuthorsId { get; set; }

        [Display(Name = "Обкладинка")]
        public HttpPostedFileBase TitlePic { get; set; }
    }
}
