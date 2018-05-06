using IPROJ.Configuration.Configurations;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.QueueManager.Connection;
using RabbitMQ.Client;

namespace IPROJ.ConnectionBroker.Managing.QueueTools
{
    public class CbConnectionFactory : IRabbitMqConnectionFactory
    {
        private ConnectionFactory _factory;

        public CbConnectionFactory(IConfigurationProvider configurationProvider)
        {
            _factory = new ConnectionFactory()
            {
                HostName = configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.MQServerIp),
                Port = ushort.Parse(configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.MQServerPort)),
                UserName = configurationProvider.GetOption(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.MQServerUser),
                Password = configurationProvider.GetOption(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.MQServerPass),
                VirtualHost = configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.MQServerVHost)
            };
        }

        public IConnection CreateConnection()
        {
            return _factory.CreateConnection();
        }
    }
}
