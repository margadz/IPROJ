using RabbitMQ.Client;
using System;

namespace IPROJ.QueueManager.Connection
{
    /// <summary>
    /// Describes abstract facility which provides connection to MqServer.
    /// </summary>
    public interface IRabbitMqConnectionFactory
    {
        /// <summary>Creates MqServer connection.</summary>
        /// <returns>Connection.</returns>
        IConnection CreateConnection();
    }
}
