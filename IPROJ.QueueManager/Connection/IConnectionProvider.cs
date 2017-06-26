using System;
using System.Collections.Generic;
using System.Text;

namespace IPROJ.QueueManager.Connection
{
    public interface IConnectionProvider
    {
        ConnectionFactory ProvideFactory();
    }
}
