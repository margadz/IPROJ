using System.Collections.Generic;
using System.Text;
using HS.QueueClient.Tools;
using IPROJ.Contracts.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HS.QueueClient
{
    public class QueueListener
    {
        private IDataRepository repository;
        private ConnectionFactory _factory;

        public QueueListener(Ic repository)
        {
            this.repository = repository;

            _factory = new ConnectionFactory()
            {
                HostName = MQServerConfig.IP,
                Port = MQServerConfig.PORT,
                UserName = MQServerConfig.USER,
                Password = MQServerConfig.PASS,
                VirtualHost = MQServerConfig.VHOST
            };
        }

        public void Run()
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.BasicQos(0, 1, false);
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(queue: MQServerConfig.ReadingsQueue, noAck: true, consumer: consumer);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.Unicode.GetString(body);
                        IList<DeviceReading> readings = QueueMessageConverter.ParseDeviceReaddings(message);
                        repository.AddReadingsAsync(readings);


                    };

                    System.Console.ReadKey();
                }
            }
        }
    }
}
