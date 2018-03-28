using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPROJ.ConnectionBroker.ClientRestApi
{
    public interface IDevicesRepository
    {
        Task<IEnumerable<Device>> GetAllDevicesAsync();
    }
}