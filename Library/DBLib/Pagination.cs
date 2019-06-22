using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib
{
    public class Pagination
    {
        public delegate int getMaxCountNumber();

        public getMaxCountNumber ElementsCountRefresh { get; set; }
        public int ElementsCount { get; set; }
        public int Step { get; set; }
        public int Page { get; set; }

        public int StartPage
        {
            get
            {
                int startPage = (Page - 1) * Step + 1;
                if (startPage > ElementsCount)
                    return ElementsCount - Step + 1;
                return startPage;
            }
        }

        public int EndPage
        {
            get
            {
                int endPage = StartPage + Step - 1;
                if (endPage > ElementsCount)
                    return ElementsCount;
                return endPage;
            }
        }

        public int GetMaxPage
        {
            get
            {
                return Convert.ToInt32(Math.Ceiling((double)ElementsCountRefresh() / (double)Step));
            }
        }

        public Pagination(int step, getMaxCountNumber maxCountRefresh)
        {
            Step = step;
            ElementsCountRefresh = maxCountRefresh;
        }

        public void setPage(int page)
        {
            Page = page < 1 ? 1 : page;
            ElementsCount = ElementsCountRefresh();
        }

        public void setPage(out int StartPage, out int EndPage, int page)
        {
            Page = page < 1 ? 1 : page;
            StartPage = this.StartPage;
            EndPage = this.EndPage;
            ElementsCount = ElementsCountRefresh();
        }
    }
}
