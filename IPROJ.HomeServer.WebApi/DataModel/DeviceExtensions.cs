using IPROJ.Contracts.DataModel;

namespace IPROJ.HomeServer.WebApi.DataModel
{
    internal static class DeviceExtensions
    {
        internal static DeviceDescription CheckParameters(this DeviceDescription deviceDescription)
        {
            if (deviceDescription.DeviceId == null || string.IsNullOrEmpty(deviceDescription.Name) || string.IsNullOrEmpty(deviceDescription.Host))
            {
                return null;
            }

            return deviceDescription;
        }
    }
}
