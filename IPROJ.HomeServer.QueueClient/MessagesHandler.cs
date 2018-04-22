using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Messaging;

namespace IPROJ.HomeServer.QueueClient
{
    public class MessagesHandler : IMessagesHandler
    {
        private readonly IDataRepository _dataRepository;
        private readonly IQueueListener _queueListener;

        public MessagesHandler(IDataRepository dataRepository, IQueueListener queueListener)
        {
            _dataRepository = dataRepository;
            _queueListener = queueListener;
            _queueListener.QueueEvent += HandleMessages;
        }

        public void StartStartHandling(CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler started listening...");
            _queueListener.Listen(cancellationToken);
        }

        private async void HandleMessages(IEnumerable<DeviceReading> readings)
        {
            var filtered = readings.Where(reading => reading != null && reading.ReadingCharacter == ReadingCharacter.Daily);
            //Console.WriteLine($"Received message with {readings.Count()} readings.");
            await _dataRepository.AddReadingsAsync(filtered);
        }
    }
}
