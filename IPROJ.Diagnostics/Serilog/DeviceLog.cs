using System;
using IPROJ.Contracts;
using IPROJ.Contracts.Logging;
using Serilog;

namespace IPROJ.Diagnostics.Serilog
{
    /// <summary>Serilog-based implementation of <see cref="IDeviceLogger"/>.</summary>
    public class DeviceLog : IDeviceLogger
    {
        /// <inheritdoc />
        public void InformDeviceHasConnected(IDevice device)
        {
            Log.Information($"InformDeviceHasConnected - Device: \"{device.DeviceName}\" has been connected");
        }

        /// <inheritdoc />
        public void RaiseErrorOnDeviceConnections(Exception error, IDevice device)
        {
            Log.Error($"RaiseErrorOnDeviceConnections - Cannot connect to: \"{device.DeviceName}\" device due to: {error.Message}");
        }

        /// <inheritdoc />
        public void RaiseErrorOnGettingData(Exception error, IDevice device)
        {
            Log.Error($"RaiseErrorOnGettingData - Cannot get readings from: \"{device.DeviceName}\" device due to: {error.Message}");
        }

        /// <inheritdoc />
        public void InformDeviceConnectionWasReestablished(IDevice device)
        {
            Log.Information($"InformDeviceConnectionWasReestablished - Connection: \"{device.DeviceName}\" has been reestablished");
        }
    }
}
