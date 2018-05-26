using System;
using IPROJ.Contracts.Device.Discovery;

namespace IPROJ.Contracts.Logging
{
    /// <summary>Describes logger for <see cref="IDeviceFind1er"/>.</summary>
    public interface IDeviceFinderLogger
    {
        void RaiseOnErrorDuringDiscover(Exception error, IDeviceFinder deviceFinder);

        void InformWhenDeviceDiscoveryHasFinished(int numberOfDevices);

        void InformWhenDeviceDiscoveryHasStarted();
    }
}
