using System.Collections.Generic;
using System.Text;
using IPROJ.Configuration.ConfigurationProvider;
using IPROJ.Configuration.Configurations;
using IPROJ.Contracts.DataModel;
using IPROJ.QueueManager.Connection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HS.QueueClient
{
    public class QueueListener
    {
        private ConnectionFactory _factory;
        private readonly string _readingQueueName;

        public QueueListener(IConnectionFactoryProvider queueConnectionProvider, IConfigurationProvider configurationProvider)
        {
            _readingQueueName = configurationProvider.GetOption<string>(CoreConfigurations.Category, CoreConfigurations.ReadingsQueue);
        
            _factory = queueConnectionProvider.ProvideFactory();
        }

        public void Run()
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.BasicQos(0, 1, false);
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(queue: _readingQueueName, noAck: true, consumer: consumer);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.Unicode.GetString(body);
                        IList<DeviceReading> readings = QueueMessageConverter.ParseDeviceReaddings(message);


                    };

                    System.Console.ReadKey();
                }
            }
        }
    }
}
