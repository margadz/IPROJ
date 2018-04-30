﻿using IPROJ.Configuration.Configurations;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.QueueManager.Connection;
using RabbitMQ.Client;

namespace IPROJ.ConnectionBroker.QueueManaging.Exchanges
{
    public class CbConnectionFactoryProvider : IConnectionFactoryProvider
    {
        private ConnectionFactory _factory;

        public CbConnectionFactoryProvider(IConfigurationProvider configurationProvider)
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

        public ConnectionFactory ProvideFactory()
        {
            return _factory;
        }
    }
}
