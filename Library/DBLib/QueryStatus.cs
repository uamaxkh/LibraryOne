using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib
{
    public class QueryStatus
    {
        public QueryStatusCode queryStatusCode { get; set; }
        public string Message { get; set; }

        public QueryStatus() { }

        public QueryStatus(QueryStatusCode queryStatusCode)
        {
            this.queryStatusCode = queryStatusCode;
        }

        public QueryStatus(QueryStatusCode queryStatusCode, string Message)
        {
            this.queryStatusCode = queryStatusCode;
            this.Message = Message;
        }
    }

    public enum QueryStatusCode
    {
        Success,
        IsExist,
        DBError,
        Error
    }
}
