using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace CB.DevicesManager
{
    public interface IDevice
    {
        Task<DeviceReading> GetInsantReading();

        Task<DeviceReading> GetHourlyReading();
    }
}
