using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Models
{
    /// <summary>
    /// Reflects Comment from DB
    /// </summary>
    public class Comment : BaseEntity
    {
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Текст коментаря")]
        public string CommentText { get; set; }

        public Book Book { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Comment()
        {
            Date = DateTime.Now;
        }
    }
}
