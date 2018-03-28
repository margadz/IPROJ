using System.Collections.Generic;
using System.Text;
using System.Threading;
using IPROJ.Configuration.Configurations;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Messaging;
using IPROJ.QueueManager.Connection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IPROJ.HomeServer.QueueClient
{
    public class QueueListener : IQueueListener
    {
        private readonly string _readingQueueName;
        private readonly Encoding _encoding;
        private ConnectionFactory _factory;
        

        public QueueListener(IConnectionFactoryProvider queueConnectionProvider, IConfigurationProvider configurationProvider)
        {
            _readingQueueName = configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.ReadingsQueue);
            _encoding = Encoding.GetEncoding(int.Parse(configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.CodePage)));
            _factory = queueConnectionProvider.ProvideFactory();
        }

        public event QueueEventHandler QueueEvent;

        public void Dispose()
        {
        }

        public void Listen(CancellationToken token)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.BasicQos(0, 1, false);
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(queue: _readingQueueName, autoAck: true, consumer: consumer);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = _encoding.GetString(body);
                        var result = JsonConvert.DeserializeObject<IEnumerable<DeviceReading>>(message);
                        QueueEvent.Invoke(result);
                    };

                    while (!token.IsCancellationRequested)
                    {
                    }
                }
            }
        }
    }
}
