using CB.Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CB.Common.Interfaces
{
    public interface IDevicesRepository
    {
        Task<IList<Device>> GetAllActiveDevicesAsync();
    }
}
