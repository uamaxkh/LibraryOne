﻿using System;
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
        
        [Display(Name = "Обкладинка")]
        [UIHint("Url")]
        public string TitlePic { get; set; }

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

        [Display(Name = "Автор(и)")]
        public virtual ICollection<Author> Authors { get; set; }
        
        public virtual ICollection<BooksRenting> BooksRenting { get; set; }

        public Book()
        {
            BooksRenting = new List<BooksRenting>();
            Authors = new List<Author>();
        }

        public Book(string ISBN, bool LibraryReading, int Pages,
            int Quantity, string Title, short Year)
        {
            this.ISBN = ISBN;
            this.LibraryReading = LibraryReading;
            this.Pages = Pages;
            this.Quantity = Quantity;
            this.Title = Title;
            this.Year = Year;

            BooksRenting = new List<BooksRenting>();
            Authors = new List<Author>();
        }
    }
}
