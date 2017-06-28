using RabbitMQ.Client;

namespace IPROJ.QueueManager.Connection
{
    public interface IConnectionFactoryProvider
    {
        ConnectionFactory ProvideFactory();
    }
}
