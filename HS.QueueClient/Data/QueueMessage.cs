using HS.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HS.QueueClient.Data
{
    public class QueueMessage : IMessage
    {
        public string Message { get; private set; }

        public string Body { get; private set; }

        public string Header { get; private set; }

        public QueueMessage(string message)
        {

        }
    }
}
