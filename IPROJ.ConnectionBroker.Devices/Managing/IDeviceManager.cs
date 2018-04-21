using System.Threading;
using System.Threading.Tasks;

namespace IPROJ.ConnectionBroker.Devices.Managing
{
    public interface IDeviceManager
    {
        Task ManageDevices(CancellationToken cancellationToken);
    }
}
