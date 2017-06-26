using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Common.Exception
{
    public class QueueMessageException : System.Exception
    {
        public QueueMessageException(string message)
            : base(message)
        {
            
        }
    }
}
