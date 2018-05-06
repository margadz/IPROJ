using IPROJ.Contracts.DataModel;

namespace IPROJ.Contracts.Devices
{
    /// <summary>Provide abstract factory of <see cref="IDevice"/> instances.</summary>
    public interface IDeviceFactory
    {
        /// <summary>Creates instance of <see cref="IDevice"/> base on provide <see cref="DeviceDescription"/>.</summary>
        /// <param name="deviceDescription">Device.</param>
        /// <returns></returns>
        IDevice CreateDevice(DeviceDescription deviceDescription);
    }
}
