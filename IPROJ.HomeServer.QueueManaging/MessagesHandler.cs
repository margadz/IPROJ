using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Messaging;

namespace IPROJ.HomeServer.QueueClient
{
    public class MessagesHandler : IMessagesHandler
    {
        private readonly IDataRepository _dataRepository;
        private readonly IQueueListener _queueListener;
        private readonly IInstantMessenger _instantMessenger;
        private CancellationToken _cancellationToken;

        public MessagesHandler(IDataRepository dataRepository, IQueueListener queueListener, IInstantMessenger instantMessenger)
        {
            Argument.OfWichValueShoulBeProvided(dataRepository, nameof(dataRepository));
            Argument.OfWichValueShoulBeProvided(queueListener, nameof(queueListener));
            Argument.OfWichValueShoulBeProvided(instantMessenger, nameof(instantMessenger));

            _dataRepository = dataRepository;
            _queueListener = queueListener;
            _instantMessenger = instantMessenger;
            _queueListener.QueueEvent += HandleMessages;
        }

        public async Task StartStartHandling(CancellationToken cancellationToken)
        {
            Console.WriteLine("Handler started listening...");
            _cancellationToken = cancellationToken;
            await _queueListener.Listen(cancellationToken);
        }

        private async void HandleMessages(IEnumerable<DeviceReading> readings)
        {
            var notNull = readings.Where(reading => reading != null);
            var daily = notNull.Where(reading => reading.ReadingCharacter == ReadingCharacter.Daily);
            var instant = notNull.Where(reading => reading.ReadingCharacter == ReadingCharacter.Instant);
            await _dataRepository.AddReadingsAsync(daily);
            await _instantMessenger.SendMessage(instant, _cancellationToken);
        }
    }
}
