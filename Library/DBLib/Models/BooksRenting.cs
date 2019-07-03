using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLib;

namespace DBLib.Models
{
    /// <summary>
    /// Reflects books renting of user from DB
    /// </summary>
    public class BooksRenting : BaseEntity
    {
        public Book Book { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? TakingDate { get; set; }

        public DateTime? ReturningDate { get; set; }

        /// <summary>
        /// Calculate date of returning of book
        /// Use LibrarySettings object
        /// </summary>
        public DateTime GetReturningDate
        {
            get
            {
                if(TakingDate != null)
                {
                    return ((DateTime)TakingDate).AddDays(LibrarySettings.getDaysForTaking());
                }
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Check user order (if he already take the book)
        /// Returns "has penalty" (true), if current date is bigger that returning date
        /// Use LibrarySettings object
        /// </summary>
        public bool IsHasPenalty
        {
            get
            {
                if (TakingDate != null && ReturningDate == null)
                {
                    var returningDate = GetReturningDate;
                    int penaltyDays = (DateTime.Now - returningDate).Days;
                    if (penaltyDays > 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }

        /// <summary>
        /// Calculate fine value for current book, that user doesn`t return in time
        /// Use LibrarySettings object
        /// </summary>
        public double GetFineValue
        {
            get
            {
                if (TakingDate != null && ReturningDate == null && GetReturningDate < DateTime.Now)
                {
                    var returningDate = GetReturningDate;
                    int penaltyDays = (DateTime.Now - returningDate).Days;
                    double fine = penaltyDays * LibrarySettings.getFinePerDay();
                    return fine;
                }
                return 0.0;
            }
        }
    }
}
