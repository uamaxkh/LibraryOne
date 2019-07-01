using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib
{
    /// <summary>
    /// Page pagination
    /// Used for books displaying
    /// </summary>
    public class Pagination
    {
        public delegate int getMaxCountNumber();

        public getMaxCountNumber ElementsCountRefresh { get; set; }
        public int ElementsCount { get; set; }
        public int Step { get; set; }
        public int Page { get; set; }

        /// <summary>
        /// Return first element for this page
        /// Check out of the range (min page)
        /// </summary>
        public int StartElement
        {
            get
            {
                int startElement = (Page - 1) * Step + 1;
                if (startElement > ElementsCount)
                    return ElementsCount - Step + 1;
                return startElement;
            }
        }

        /// <summary>
        /// Return last element for this page
        /// Check out of the range (max page)
        /// </summary>
        public int EndElement
        {
            get
            {
                int endElement = StartElement + Step - 1;
                if (endElement > ElementsCount)
                    return ElementsCount;
                return endElement;
            }
        }

        /// <summary>
        /// Return last page for this step
        /// </summary>
        public int GetMaxPage
        {
            get
            {
                return Convert.ToInt32(Math.Ceiling((double)ElementsCountRefresh() / (double)Step));
            }
        }

        /// <summary>
        /// Initialize Pagination class
        /// </summary>
        /// <param name="maxCountRefresh">Method, that return max count of elements</param>
        public Pagination(int step, getMaxCountNumber maxCountRefresh)
        {
            Step = step;
            ElementsCountRefresh = maxCountRefresh;
        }

        /// <summary>
        /// Set start & end elements for page by current page number
        /// </summary>
        public void setPage(int page)
        {
            Page = page < 1 ? 1 : page;
            ElementsCount = ElementsCountRefresh();
        }

        /// <summary>
        /// Set start & end elements for page by current page number
        /// with OUT parameters
        /// </summary>
        public void setPage(out int StartElement, out int EndElement, int page)
        {
            Page = page < 1 ? 1 : page;
            StartElement = this.StartElement;
            EndElement = this.EndElement;
            ElementsCount = ElementsCountRefresh();
        }
    }
}
