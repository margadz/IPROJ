using System;
using IPROJ.Configuration.ConfigurationProvider;
using IPROJ.Configuration.Configurations;
using IPROJ.QueueManager.Connection;
using RabbitMQ.Client;

namespace CB.QueueManaging.Exchanges
{
    class CbConnectionFactoryProvider : IConnectionFactoryProvider
    {
        ConnectionFactory _factory;

        public CbConnectionFactoryProvider(IConfigurationProvider configurationProvider)
        {
            _factory = new ConnectionFactory()
            {
                HostName = configurationProvider.GetOption<string>(CoreConfigurations.Category, CoreConfigurations.MQServerIp),
                Port = configurationProvider.GetOption<ushort>(CoreConfigurations.Category, CoreConfigurations.MQServerPort),
                UserName = configurationProvider.GetOption<string>(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.MQServerUser),
                Password = configurationProvider.GetOption<string>(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.MQServerPass),
                VirtualHost = configurationProvider.GetOption<string>(CoreConfigurations.Category, CoreConfigurations.MQServerVHost)
            };

        }

        public ConnectionFactory ProvideFactory()
        {
            throw new NotImplementedException();
        }
    }
}
