using System;
using IPROJ.Contracts;
using IPROJ.Contracts.Logging;
using Serilog;

namespace IPROJ.Diagnostics.Serilog
{
    public class DeviceLog : IDeviceLog
    {
        public void InformDeviceHasConnected(IDevice device)
        {
            Log.Information($"InformDeviceHasConnected - Device: \"{device.DeviceName}\" has been connected");
        }

        public void RaiseErrorOnDeviceConnections(Exception error, IDevice device)
        {
            Log.Information($"RaiseErrorOnDeviceConnections - Cannot connect to: \"{device.DeviceName}\" device due to: {error.Message}");
        }

        public void RaiseErrorOnGettingData(Exception error, IDevice device)
        {
            Log.Information($"RaiseErrorOnGettingData - Cannot get readings from: \"{device.DeviceName}\" device due to: {error.Message}");
        }

        public void InformDeviceConnectionWasReestablished(IDevice device)
        {
            Log.Information($"InformDeviceConnectionWasReestablished - Connection: \"{device.DeviceName}\" has been reestablished");
        }
    }
}
