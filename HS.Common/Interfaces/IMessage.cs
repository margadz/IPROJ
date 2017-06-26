using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Common.Interfaces
{
    public interface IMessage
    {
        string Message { get; }

        string Body { get; }

        string Header { get; }
    }
}
