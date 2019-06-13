using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace DBLib.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string CommentText { get; set; }
        public Book Book { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
