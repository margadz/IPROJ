using RabbitMQ.Client;
using System;

namespace IPROJ.QueueManager.Connection
{
    public interface IConnectionFactoryProvider
    {
        ConnectionFactory ProvideFactory();

        IConnection CreateConnection();
    }
}
