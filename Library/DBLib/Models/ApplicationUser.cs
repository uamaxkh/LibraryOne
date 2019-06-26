using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using DBLib.Models;

//namespace DBLib.Models
 namespace Library.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Прізвище")]
        public string Surname { get; set; }

        [Display(Name = "Заблокований")]
        [UIHint("Boolean")]
        public bool IsBanned { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<BooksRenting> BooksRenting { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public ApplicationUser()
        {
            Comments = new List<Comment>();
            BooksRenting = new List<BooksRenting>();
            IsBanned = false;
        }

        public bool userHasPenalty
        {
            get
            {
                foreach(var bookRenting in BooksRenting)
                {
                    if (bookRenting.isHasPenalty)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public double getSummaryFineValue
        {
            get
            {
                double summaryFine = 0;
                foreach (var bookRenting in BooksRenting)
                {
                    //if (bookRenting.isHasPenalty)
                    //{
                        summaryFine += bookRenting.getFineValue;
                    //}
                }
                return summaryFine;
            }
        }
    }
}
