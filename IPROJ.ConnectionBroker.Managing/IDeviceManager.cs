using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPROJ.ConnectionBroker.Managing
{
    /// <summary>Decribes abstract devices manager.</summary>
    public interface IDeviceManager
    {
        /// <summary>Starts managing devices.</summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task from the operation.</returns>
        Task Manage(CancellationToken cancellationToken);
    }
}
