using System;

namespace IPROJ.Contracts.Logging
{
    public interface IDeviceLog
    {
        void InformDeviceHasConnected(IDevice device);

        void RaiseErrorOnDeviceConnections(Exception error, IDevice device);

        void RaiseErrorOnGettingData(Exception error, IDevice device);

        void InformDeviceConnectionWasReestablished(IDevice device);
    }
}
