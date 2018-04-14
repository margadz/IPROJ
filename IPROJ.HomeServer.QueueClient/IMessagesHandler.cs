using System.Threading;

namespace IPROJ.HomeServer.QueueClient
{
    public interface IMessagesHandler
    {
        void StartStartHandling(CancellationToken cancellationToken);
    }
}