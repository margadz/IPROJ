﻿using System;
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
        private readonly CancellationToken _cancellationToken;

        public MessagesHandler(IDataRepository dataRepository, IQueueListener queueListener)
        {
            _dataRepository = dataRepository;
            _queueListener = queueListener;
            _queueListener.QueueEvent += HandleMessages;
        }

        public void StartListening(CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler started listening...");
            _queueListener.Listen(cancellationToken);
        }

        private async void HandleMessages(IEnumerable<DeviceReading> readings)
        {
            Console.WriteLine($"Received message with {readings.Count()} readings.");
            var filtered = readings.Where(reading => reading != null);
            await _dataRepository.AddReadingsAsync(filtered);
        }
    }
}
