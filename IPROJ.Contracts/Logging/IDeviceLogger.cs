using System;

namespace IPROJ.Contracts.Logging
{
    /// <summary>Describes logger for <see cref="IDevice"/>.</summary>
    public interface IDeviceLogger
    {
        void InformDeviceHasConnected(IDevice device);

        void RaiseErrorOnDeviceConnections(Exception error, IDevice device);

        void RaiseErrorOnGettingData(Exception error, IDevice device);

        void InformDeviceConnectionWasReestablished(IDevice device);
    }
}
