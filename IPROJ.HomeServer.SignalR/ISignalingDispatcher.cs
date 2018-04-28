using System;
using System.Threading;

namespace IPROJ.HomeServer.SignalR
{
    public interface ISignalingDispatcher : IDisposable
    {
        void StartDispatching(CancellationToken cancellationToken);
    }
}