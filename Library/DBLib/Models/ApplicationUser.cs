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

namespace DBLib.Models
{
    /// <summary>
    /// Standart ApplicationUser class with some changes for library project
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необхідне поле")]
        [Display(Name = "Прізвище")]
        public string Surname { get; set; }

        [Display(Name = "Заблокований")]
        [UIHint("Boolean")]
        public bool IsBanned { get; set; }

        public DateTime RegistrationDate { get; set; }

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
            RegistrationDate = DateTime.Now;
        }

        /// <summary>
        /// Returns "has penalty" (true), if user has at least one penalty order
        /// </summary>
        public bool userHasPenalty
        {
            get
            {
                foreach(var bookRenting in BooksRenting)
                {
                    if (bookRenting.IsHasPenalty)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Returns summary fine for all penalty orders
        /// </summary>
        public double getSummaryFineValue
        {
            get
            {
                double summaryFine = 0;
                foreach (var bookRenting in BooksRenting)
                {
                    //if (bookRenting.IsHasPenalty)
                    //{
                        summaryFine += bookRenting.GetFineValue;
                    //}
                }
                return summaryFine;
            }
        }

        /// <summary>
        /// Return count of books, that were taked, but not returned
        /// </summary>
        public int getCountOfTakedBooks
        {
            get
            {
                return BooksRenting.Where(br => br.TakingDate != null && br.ReturningDate == null).Count();
            }
        }

        /// <summary>
        /// Return count of books, that were ordered, but not taked
        /// </summary>
        public int getCountOfOrderedBooks
        {
            get
            {
                return BooksRenting.Where(br => br.TakingDate == null).Count();
            }
        }
    }
}
