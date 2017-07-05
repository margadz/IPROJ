using System;
using System.Collections.Generic;
using System.Text;
using IPROJ.Configuration.ConfigurationProvider;
using IPROJ.Configuration.Configurations;
using IPROJ.QueueManager.Connection;
using RabbitMQ.Client;

namespace HS.QueueClient
{
    public class HsConnectionFactoryProvider : IConnectionFactoryProvider
    {
        private ConnectionFactory _factory;

        public HsConnectionFactoryProvider(IConfigurationProvider configurationProvider)
        {
            _factory = new ConnectionFactory()
            {
                HostName = configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.MQServerIp),
                Port = ushort.Parse(configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.MQServerPort)),
                UserName = configurationProvider.GetOption(HomeServerConfiguration.Category, ConnectionBrokerConfigurations.MQServerUser),
                Password = configurationProvider.GetOption(HomeServerConfiguration.Category, ConnectionBrokerConfigurations.MQServerPass),
                VirtualHost = configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.MQServerVHost)
            };
        }

        public ConnectionFactory ProvideFactory()
        {
            return _factory;
        }
    }
}
