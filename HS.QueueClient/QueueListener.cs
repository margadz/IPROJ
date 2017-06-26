using System.Collections.Generic;
using HS.Common.Interfaces;
using System.Threading.Tasks;
using RabbitMQ.Client;
using HS.Common.ConfigData;
using RabbitMQ.Client.Events;
using HS.Common.OutputModel;
using System.Text;
using HS.QueueClient.Tools;

namespace HS.QueueClient
{
    public class QueueListener
    {
        private IDataRepository repository;
        private ConnectionFactory _factory;

        public QueueListener(IDataRepository repository)
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
