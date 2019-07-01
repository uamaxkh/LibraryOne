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
    /// <summary>
    /// Reflects book from View for further saving in DB
    /// </summary>
    public class AddBooksViewModels
    {
        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Необхідне поле")]
        [Range(1900, 3000, ErrorMessage = "Рік не може бути меншим за {1}")]
        [Display(Name = "Рік видання")]
        public short Year { get; set; }

        [Display(Name = "Читання в бібліотеці")]
        [UIHint("Boolean")]
        public bool LibraryReading { get; set; }

        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Кількість книг")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} не може бути менша за {1}")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Сторінок")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} не може бути менше за {1}")]
        public int Pages { get; set; }

        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Код ISBN")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "А де ж назва розділу?")]
        [Display(Name = "Назва розділу")]
        public string SectionName { get; set; }

        [Required(ErrorMessage = "Як же без видавця")]
        [Display(Name = "Видавець")]
        public string PublisherName { get; set; }

        [Required(ErrorMessage = "Хоча б один автор має бути")]
        [Display(Name = "Автори")]
        public virtual ICollection<Guid> AuthorsId { get; set; }

        [Display(Name = "Обкладинка")]
        public HttpPostedFileBase TitlePic { get; set; }
    }
}
