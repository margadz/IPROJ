using IPROJ.Configuration.Configurations;
using IPROJ.Contracts.ConfigurationProvider;
using RabbitMQ.Client;

namespace IPROJ.HomeServer.QueueClient
{
    /// <summary>Provides connection factory for HomeServer application.</summary>
    public class HsConnectionFactory : QueueManager.Connection.IRabbitMqConnectionFactory
    {
        private readonly ConnectionFactory _factory;

        /// <summary>Initialize instance of <see cref="HsConnectionFactory"/>.</summary>
        /// <param name="configurationProvider">Configuration provider.</param>
        public HsConnectionFactory(IConfigurationProvider configurationProvider)
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

        /// <summary>Creates MqServer connection.</summary>
        /// <returns>Connection.</returns>
        public IConnection CreateConnection()
        {
            return _factory.CreateConnection();
        }
    }
}
