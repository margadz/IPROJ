using System;
using IPROJ.Contracts.Device.Discovery;
using IPROJ.Contracts.Logging;
using Serilog;

namespace IPROJ.Diagnostics.Serilog
{
    /// <summary>Serilog-based implementation of <see cref="IDeviceFinderLogger"/>.</summary>
    public class DeviceFinderLogger : IDeviceFinderLogger
    {
        public void InformWhenDiscoveryHasFinished(int numberOfDevices)
        {
            Log.Information($"InformWhenDiscoveryHasFinished - Device finder disceverd {numberOfDevices} of devices.");
        }

        /// <inheritdoc />
        public void RaiseOnErrorDuringDiscover(Exception error, IDeviceFinder deviceFinder)
        {
            Log.Error($"RaiseOnErrorDuringDiscover - Error during discovery by: \"{deviceFinder.GetType().Name}\" due to: {error.Message}");
        }
    }
}
