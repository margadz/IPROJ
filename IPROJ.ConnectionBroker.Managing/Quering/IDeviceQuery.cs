using System.Threading;
using System.Threading.Tasks;

namespace IPROJ.ConnectionBroker.Managing.Quering
{
    /// <summary>Describes abstract device managing facility.</summary>
    public interface IDeviceQuery
    {
        /// <summary>Starts managing of devices according to implementation.</summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task of the operation.</returns>
        Task QueryDevices(CancellationToken cancellationToken);
    }
}
