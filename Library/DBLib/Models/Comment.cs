using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace DBLib.Models
{
    public class Comment : BaseEntity
    {
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Текст коментаря")]
        public string CommentText { get; set; }

        //[Required(ErrorMessage = "Необхідне поле")]
        //[Range(1, 5, ErrorMessage = "Оцінка може бути від 1 до 5")]
        //[Display(Name = "Оцінка книги")]
        //public int BookRate {
        //    get
        //    {
        //        return BookRate;
        //    }
        //    set
        //    {
        //        if (value > 5)
        //        {
        //            BookRate = 5;
        //        }
        //        else if (value < 1)
        //        {
        //            BookRate = 1;
        //        }
        //        else
        //        {
        //            BookRate = value;
        //        }
        //    }
        //}

        public Book Book { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Comment()
        {
            Date = DateTime.Now;
        }
    }
}
