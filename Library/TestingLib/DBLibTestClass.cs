using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLib;
using DBLib.Models;

namespace TestingLib
{

    [TestFixture]
    public class DBLibPaginationTestClass
    {
        Pagination paination;

        public int MaxCount()
        {
            return 50;
        }

        [SetUp]
        public void SetPagination()
        {
            paination = new Pagination(10, MaxCount);
        }

        [TestCase(-10, ExpectedResult = 1)]
        [TestCase(-1, ExpectedResult = 1)]
        [TestCase(0, ExpectedResult = 1)]
        public int StartPage_OutOfTheRangePageMin_FirstElement(int currentPage)
        {
            paination.setPage(currentPage);
            return paination.StartElement;
        }

        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 11)]
        [TestCase(3, ExpectedResult = 21)]
        [TestCase(4, ExpectedResult = 31)]
        [TestCase(5, ExpectedResult = 41)]
        public int StartPage_NormalCurrentPage(int currentPage)
        {
            paination.setPage(currentPage);
            return paination.StartElement;
        }

        [TestCase(6, ExpectedResult = 41)]
        [TestCase(7, ExpectedResult = 41)]
        public int StartPage_OutOfTheRangePageMax_MaxMinusStepElement(int currentPage)
        {
            paination.setPage(currentPage);
            return paination.StartElement;
        }

        [TestCase(-10, ExpectedResult = 1)]
        [TestCase(-1, ExpectedResult = 1)]
        [TestCase(0, ExpectedResult = 1)]
        public int StartPageWithOutParameters_OutOfTheRangePageMin_FirstElement(int currentPage)
        {
            int startPage, endPage;
            paination.setPage(out startPage, out endPage, currentPage);
            return startPage;
        }

        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 11)]
        [TestCase(3, ExpectedResult = 21)]
        [TestCase(4, ExpectedResult = 31)]
        [TestCase(5, ExpectedResult = 41)]
        public int StartPageWithOutParameters_NormalCurrentPage(int currentPage)
        {
            int startPage, endPage;
            paination.setPage(out startPage, out endPage, currentPage);
            return startPage;
        }

        [TestCase(6, ExpectedResult = 41)]
        [TestCase(7, ExpectedResult = 41)]
        public int StartPageWithOutParameters_OutOfTheRangePageMax_MaxMinusStepElement(int currentPage)
        {
            int startPage, endPage;
            paination.setPage(out startPage, out endPage, currentPage);
            return startPage;
        }

        [TestCase(-10, ExpectedResult = 10)]
        [TestCase(-1, ExpectedResult = 10)]
        [TestCase(0, ExpectedResult = 10)]
        public int EndPage_OutOfTheRangePageMin_FirstElementPlusStep(int currentPage)
        {
            paination.setPage(currentPage);
            return paination.EndElement;
        }

        [TestCase(1, ExpectedResult = 10)]
        [TestCase(2, ExpectedResult = 20)]
        [TestCase(3, ExpectedResult = 30)]
        [TestCase(4, ExpectedResult = 40)]
        [TestCase(5, ExpectedResult = 50)]
        public int EndPage_NormalCurrentPage(int currentPage)
        {
            paination.setPage(currentPage);
            return paination.EndElement;
        }

        [TestCase(6, ExpectedResult = 50)]
        [TestCase(7, ExpectedResult = 50)]
        public int EndPage_OutOfTheRangePageMax_LastElement(int currentPage)
        {
            paination.setPage(currentPage);
            return paination.EndElement;
        }

        [TestCase(-10, ExpectedResult = 10)]
        [TestCase(-1, ExpectedResult = 10)]
        [TestCase(0, ExpectedResult = 10)]
        public int EndPageWithOutParameters_OutOfTheRangePageMin_FirstElementPlusStep(int currentPage)
        {
            int startPage, endPage;
            paination.setPage(out startPage, out endPage, currentPage);
            return endPage;
        }

        [TestCase(1, ExpectedResult = 10)]
        [TestCase(2, ExpectedResult = 20)]
        [TestCase(3, ExpectedResult = 30)]
        [TestCase(4, ExpectedResult = 40)]
        [TestCase(5, ExpectedResult = 50)]
        public int EndPageWithOutParameters_NormalCurrentPage(int currentPage)
        {
            int startPage, endPage;
            paination.setPage(out startPage, out endPage, currentPage);
            return endPage;
        }

        [TestCase(6, ExpectedResult = 50)]
        [TestCase(7, ExpectedResult = 50)]
        public int EndPageWithOutParameters_OutOfTheRangePageMax_LastElement(int currentPage)
        {
            int startPage, endPage;
            paination.setPage(out startPage, out endPage, currentPage);
            return endPage;
        }
    }

    [TestFixture]
    public class DBLibBooksRentingTestClass
    {
        BooksRenting booksRenting;

        [SetUp]
        public void SetBooksRenting()
        {
            booksRenting = new BooksRenting();
        }

        [TestCase("2020.02.19")]
        [TestCase("2019.07.03")]
        [TestCase("2019.12.21")]
        public void ReturningDate_TakingDatePlusGetDaysForTaking(string date)
        {
            booksRenting.TakingDate = DateTime.Parse(date);
            DateTime returningDate = booksRenting.GetReturningDate;

            DateTime returningDateCalc = ((DateTime)booksRenting.TakingDate).AddDays(LibrarySettings.getDaysForTaking());

            Assert.AreEqual(returningDate.ToString("yyyy.MM.dd"), returningDateCalc.ToString("yyyy.MM.dd"));
        }

        [TestCase(ExpectedResult = false)]
        public bool IsHasPenalty_DateWithoutPenalty_false()
        {
            booksRenting.TakingDate = DateTime.Now.AddDays(-LibrarySettings.getDaysForTaking());
            bool isHasPenalty = booksRenting.IsHasPenalty;

            return isHasPenalty;
        }

        [TestCase(ExpectedResult = false)]
        public bool IsHasPenalty_DateWithoutPenaltyPlusDay_false()
        {
            booksRenting.TakingDate = DateTime.Now.AddDays(-LibrarySettings.getDaysForTaking() + 1);
            bool isHasPenalty = booksRenting.IsHasPenalty;

            return isHasPenalty;
        }

        [TestCase(ExpectedResult = true)]
        public bool IsHasPenalty_DateWithPenalty_true()
        {
            booksRenting.TakingDate = DateTime.Now.AddDays(- LibrarySettings.getDaysForTaking() - 1);
            bool isHasPenalty = booksRenting.IsHasPenalty;

            return isHasPenalty;
        }

        [TestCase(ExpectedResult = false)]
        public bool IsHasPenaltyWithReturningDate_SetReturningDate_false()
        {
            booksRenting.TakingDate = DateTime.Now.AddDays(LibrarySettings.getDaysForTaking() - 1);
            booksRenting.ReturningDate = DateTime.Now.AddDays(-2);
            bool isHasPenalty = booksRenting.IsHasPenalty;

            return isHasPenalty;
        }

        [TestCase(ExpectedResult = 0.0)]
        public double GetFineValue_SetReturningDay_NoFine()
        {
            booksRenting.TakingDate = DateTime.Now.AddDays(LibrarySettings.getDaysForTaking() - 1);
            booksRenting.ReturningDate = DateTime.Now.AddDays(-2);
            return booksRenting.GetFineValue;
        }

        [TestCase(ExpectedResult = 0.0)]
        public double GetFineValue_NoTakingDate_NoFine()
        {
            booksRenting.OrderDate = DateTime.Now.AddDays(LibrarySettings.getDaysForTaking() - 1);
            return booksRenting.GetFineValue;
        }

        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void GetFineValue_NoPenaltyDays_NoFine(int daysWithFine)
        {
            int daysAfterTaking = LibrarySettings.getDaysForTaking() + daysWithFine;
            booksRenting.TakingDate = DateTime.Now.AddDays(-daysAfterTaking);
            double fine = booksRenting.GetFineValue;

            Assert.AreEqual(fine, 0.0);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void GetFineValue_OnTakingDate_WithFine(int daysWithFine)
        {
            int daysAfterTaking = LibrarySettings.getDaysForTaking() + daysWithFine;
            booksRenting.TakingDate = DateTime.Now.AddDays(-daysAfterTaking);
            double fine = booksRenting.GetFineValue;

            Assert.AreEqual(fine, daysWithFine * LibrarySettings.getFinePerDay());
        }
    }
}
