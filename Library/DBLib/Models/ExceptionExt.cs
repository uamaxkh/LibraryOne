using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Models
{
    public class ExceptionExt : Exception
    {
        public string MessageExt { get; set; }
        public string AdditionalInfo { get; set; }
        public MessageState State { get; set; }

        public ExceptionExt()
        : base(){ }

        public ExceptionExt(Exception ex)
        : base() {
            MessageExt = ex.Message;
            AdditionalInfo = ex.StackTrace;
            State = MessageState.Error;
        }

        public ExceptionExt(string MessageExt)
        : base(){
            this.MessageExt = MessageExt;
        }

        public ExceptionExt(string MessageExt, string AdditionalInfo)
        : base()
        {
            this.MessageExt = MessageExt;
            this.AdditionalInfo = AdditionalInfo;
        }

        public ExceptionExt(string MessageExt, string AdditionalInfo, MessageState State)
        : base()
        {
            this.MessageExt = MessageExt;
            this.AdditionalInfo = AdditionalInfo;
            this.State = State;
        }

        public override string ToString()
        {
            return MessageExt;
        }
    }

    public enum MessageState
    {
        Error,
        Info,
        Succes
    }
}
