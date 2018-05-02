using System;
using System.Threading;

namespace IPROJ.Contracts.Threading
{
    public interface IThreadingInfrastructure : IDisposable
    {
        CancellationToken CancellationToken { get; }

        void Cancel();
    }
}
