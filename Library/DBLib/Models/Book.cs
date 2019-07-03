using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Models
{
    /// <summary>
    /// Reflects books from DB
    /// </summary>
    public class Book : BaseEntity
    {
        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Рік видання")]
        [Range(1900, 3000, ErrorMessage = "{0} не може бути меншим за {1}")]
        public short Year { get; set; }
        
        [Display(Name = "Читання в бібліотеці")]
        [UIHint("Boolean")]
        public bool LibraryReading { get; set; }

        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Кількість книг")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} не може бути менша за {1}")]
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
        public Section Section { get; set; }

        [Required(ErrorMessage = "Як же без видавця")]
        [Display(Name = "Видавець")]
        public Publisher Publisher { get; set; }

        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Дата додавання")]
        public DateTime AddingDate { get; set; }
        
        public bool Deleted { get; set; }

        [Display(Name = "Автор(и)")]
        public virtual List<Author> Authors { get; set; }
        
        public virtual List<BooksRenting> BooksRenting { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public Book()
        {
            AddingDate = DateTime.Now;
            BooksRenting = new List<BooksRenting>();
            Authors = new List<Author>();
            Comments = new List<Comment>();
            Deleted = false;
        }

        public Book(string ISBN, string Title, bool LibraryReading, int Pages,
            int Quantity, short Year) : this()
        {
            this.ISBN = ISBN;
            this.Title = Title;
            this.LibraryReading = LibraryReading;
            this.Pages = Pages;
            this.Quantity = Quantity;
            this.Year = Year;
        }

        public int FreeBooksCount()
        {
            return DBCommands.FreeBookCountById(Id);
        }

        /// <summary>
        /// Used for sorting by (first) author name
        /// </summary>
        public string getFirstAuthorName
        {
            get
            {
                return Authors[0].Name;
            }
        }

        public string CutTitle(int maxLenght)
        {
                if(Title.Length > (maxLenght + 3))
                {
                    return Title.Substring(0, maxLenght) + "...";
                }
                else
                {
                    return Title;
                }
        }

        public bool IsNew
        {
            get
            {
                return (DateTime.Now - AddingDate).Days < 10 ?  true : false;
            }
        }
    }
}
