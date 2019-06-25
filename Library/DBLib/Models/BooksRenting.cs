using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using DBLib;

namespace DBLib.Models
{
    public class BooksRenting : BaseEntity
    {
        public Book Book { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? TakingDate { get; set; }

        public DateTime? ReturningDate { get; set; }

        public DateTime getReturningDate
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

        public bool isHavePenalty
        {
            get
            {
                if (TakingDate != null && ReturningDate == null)
                {
                    var returningDate = ((DateTime)TakingDate).AddDays(LibrarySettings.getDaysForTaking());
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

        public double getFineValue
        {
            get
            {
                if (TakingDate != null && ReturningDate == null && getReturningDate < DateTime.Now)
                {
                    var returningDate = ((DateTime)TakingDate).AddDays(LibrarySettings.getDaysForTaking());
                    int penaltyDays = (DateTime.Now - returningDate).Days;
                    double fine = penaltyDays * LibrarySettings.getFinePerDay();
                    return fine;
                }
                return 0.0;
            }
        }
    }
}
