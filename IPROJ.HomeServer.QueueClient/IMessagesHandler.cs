using System.Threading;

namespace IPROJ.HomeServer.QueueClient
{
    public interface IMessagesHandler
    {
        void StartListening(CancellationToken cancellationToken);
    }
}