using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace DBLib.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Текст коментаря")]
        public string CommentText { get; set; }

        public Book Book { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
