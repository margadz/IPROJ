using System;
using System.Threading;
using System.Threading.Tasks;

namespace IPROJ.Contracts
{
    public interface IStartup : IDisposable
    {
        Task Start(CancellationToken cancellationToken);
    }
}
    