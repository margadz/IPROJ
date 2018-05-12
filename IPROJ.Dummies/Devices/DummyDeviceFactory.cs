using IPROJ.Contracts;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Devices;

namespace IPROJ.Dummies.Devices
{
    public class DummyDeviceFactory : IDeviceFactory
    {
        public IDevice CreateDevice(DeviceDescription deviceDescription)
        {
            return new DummyDevice(deviceDescription);
        }
    }
}
