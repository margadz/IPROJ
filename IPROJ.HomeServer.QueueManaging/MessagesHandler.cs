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

        public MessagesHandler(IDataRepository dataRepository, IQueueListener queueListener)
        {
            Argument.OfWichValueShoulBeProvided(dataRepository, nameof(dataRepository));
            Argument.OfWichValueShoulBeProvided(queueListener, nameof(queueListener));

            _dataRepository = dataRepository;
            _queueListener = queueListener;
            _queueListener.OnMessegeReceived += HandleMessages;
        }

        public async Task StartStartHandling(CancellationToken cancellationToken)
        {
            await _queueListener.Listen(cancellationToken);
        }

        private async void HandleMessages(IEnumerable<DeviceReading> readings)
        {
            var daily = readings.Where(reading => reading != null && reading.ReadingCharacter == ReadingCharacter.Daily);
            await _dataRepository.AddReadingsAsync(daily);
        }
    }
}
