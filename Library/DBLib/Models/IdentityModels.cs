using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using DBLib.Models;
using DBLib;

namespace Library.Models
{
    /// <summary>
    /// Create an application database context
    /// Stored collections of all DB elements
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BooksRenting> BooksRenting { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Section> Sections { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}