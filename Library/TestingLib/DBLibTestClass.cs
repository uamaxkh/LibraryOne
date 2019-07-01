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
    public class DBLibTestClass
    {
        #region Pagination test
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
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 11)]
        [TestCase(3, ExpectedResult = 21)]
        [TestCase(4, ExpectedResult = 31)]
        [TestCase(5, ExpectedResult = 41)]
        [TestCase(6, ExpectedResult = 41)]
        [TestCase(7, ExpectedResult = 41)]
        public int TestStartPage(int currentPage)
        {
            paination.setPage(currentPage);
            return paination.StartElement;
        }

        [TestCase(-10, ExpectedResult = 1)]
        [TestCase(-1, ExpectedResult = 1)]
        [TestCase(0, ExpectedResult = 1)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 11)]
        [TestCase(3, ExpectedResult = 21)]
        [TestCase(4, ExpectedResult = 31)]
        [TestCase(5, ExpectedResult = 41)]
        [TestCase(6, ExpectedResult = 41)]
        [TestCase(7, ExpectedResult = 41)]
        public int TestStartPageWithOutParameters(int currentPage)
        {
            int startPage, endPage;
            paination.setPage(out startPage, out endPage, currentPage);
            return startPage;
        }

        [TestCase(-10, ExpectedResult = 10)]
        [TestCase(-1, ExpectedResult = 10)]
        [TestCase(0, ExpectedResult = 10)]
        [TestCase(1, ExpectedResult = 10)]
        [TestCase(2, ExpectedResult = 20)]
        [TestCase(3, ExpectedResult = 30)]
        [TestCase(4, ExpectedResult = 40)]
        [TestCase(5, ExpectedResult = 50)]
        [TestCase(6, ExpectedResult = 50)]
        [TestCase(7, ExpectedResult = 50)]
        public int TestEndPage(int currentPage)
        {
            paination.setPage(currentPage);
            return paination.EndElement;
        }

        [TestCase(-10, ExpectedResult = 10)]
        [TestCase(-1, ExpectedResult = 10)]
        [TestCase(0, ExpectedResult = 10)]
        [TestCase(1, ExpectedResult = 10)]
        [TestCase(2, ExpectedResult = 20)]
        [TestCase(3, ExpectedResult = 30)]
        [TestCase(4, ExpectedResult = 40)]
        [TestCase(5, ExpectedResult = 50)]
        [TestCase(6, ExpectedResult = 50)]
        [TestCase(7, ExpectedResult = 50)]
        public int TestFinishPageWithOutParameters(int currentPage)
        {
            int startPage, endPage;
            paination.setPage(out startPage, out endPage, currentPage);
            return endPage;
        }
        #endregion

        #region BooksRenting test
        BooksRenting booksRenting;

        [SetUp]
        public void SetBooksRenting()
        {
            booksRenting = new BooksRenting();
            booksRenting.TakingDate = DateTime.Now;
            booksRenting.OrderDate = DateTime.Now;
            booksRenting.ReturningDate = DateTime.Now;
        }

        [TestCase(-10, ExpectedResult = 1)]
        [TestCase(-1, ExpectedResult = 1)]
        [TestCase(0, ExpectedResult = 1)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 11)]
        [TestCase(3, ExpectedResult = 21)]
        [TestCase(4, ExpectedResult = 31)]
        [TestCase(5, ExpectedResult = 41)]
        [TestCase(6, ExpectedResult = 41)]
        [TestCase(7, ExpectedResult = 41)]
        public int TestStartPage(int currentPage)
        {
            paination.setPage(currentPage);
            return paination.StartElement;
        }
        #endregion
    }
}
