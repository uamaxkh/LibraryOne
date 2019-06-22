using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Models
{
    public class Book : BaseEntity
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
        public Section Section { get; set; }

        [Required]
        [Display(Name = "Видавець")]
        public Publisher Publisher { get; set; }

        [Required]
        [Display(Name = "Дата додавання")]
        public DateTime AddingDate { get; set; }

        [Display(Name = "Автор(и)")]
        public virtual List<Author> Authors { get; set; }
        
        public virtual List<BooksRenting> BooksRenting { get; set; }

        public Book()
        {
            AddingDate = DateTime.Now;
            BooksRenting = new List<BooksRenting>();
            Authors = new List<Author>();
        }

        public Book(string ISBN, string Title, bool LibraryReading, int Pages,
            int Quantity, short Year)
        {
            this.AddingDate = DateTime.Now;
            this.ISBN = ISBN;
            this.Title = Title;
            this.LibraryReading = LibraryReading;
            this.Pages = Pages;
            this.Quantity = Quantity;
            this.Year = Year;

            BooksRenting = new List<BooksRenting>();
            Authors = new List<Author>();
        }

        public int FreeBooksCount()
        {
            return DBCommands.FreeBookCountById(Id);
        }

        public string getFirstAuthorName
        {
            get
            {
                return Authors[0].Name;
            }
        }
    }
}
