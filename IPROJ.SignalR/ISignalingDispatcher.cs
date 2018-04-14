using System;
using System.Threading;

namespace IPROJ.SignalR
{
    public interface ISignalingDispatcher : IDisposable
    {
        void StartDispatching(CancellationToken cancellationToken);
    }
}