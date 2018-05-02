using System.Threading;
using System.Threading.Tasks;

namespace IPROJ.HomeServer.QueueClient
{
    public interface IMessagesHandler
    {
        Task StartStartHandling(CancellationToken cancellationToken);
    }
}