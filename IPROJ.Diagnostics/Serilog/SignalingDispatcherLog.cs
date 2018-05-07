using System;
using IPROJ.Contracts.Logging;
using Serilog;

namespace IPROJ.Diagnostics.Serilog
{ 
    /// <summary>Serilog-based implementation of <see cref="IQueueLogger"/>.</summary>
    public class SignalRMessengerLogger : IInstantMessengerLog
    {
        /// <inheritdoc />
        public void InformDispacherIsConnectingToHub()
        {
            Log.Information("InformDispacherIsConnectingToHub");
        }

        /// <inheritdoc />
        public void InformDispatcherConnectedToHub()
        {
            Log.Information("InformDispatcherConnectedToHub");
        }

        /// <inheritdoc />
        public void RaiseDispatcherHubConnectionError(Exception error)
        {
            Log.Error($"RaiseDispatcherHubConnectionError due to: {error.Message}");
        }
    }
}
