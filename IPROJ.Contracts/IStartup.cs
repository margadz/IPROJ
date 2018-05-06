using System;
using System.Threading;
using System.Threading.Tasks;

namespace IPROJ.Contracts
{
    /// <summary>Describes abstract entry point.</summary>
    public interface IStartup : IDisposable
    {
        /// <summary>Starts the application.</summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task from the operation.</returns>
        Task Start(CancellationToken cancellationToken);
    }
}
    