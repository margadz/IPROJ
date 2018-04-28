using System;
using IPROJ.Contracts.Logging;
using Serilog;

namespace IPROJ.Diagnostics.Serilog
{
    public class SignalingDispatcherLog : ISignalingDispatcherLog
    {
        public void InformDispacherIsConnectingToHub()
        {
            Log.Information("InformDispacherIsConnectingToHub");
        }

        public void InformDispatcherConnectedToHub()
        {
            Log.Information("InformDispatcherConnectedToHub");
        }

        public void RaiseDispatcherHubConnectionError(Exception error)
        {
            Log.Error($"RaiseDispatcherHubConnectionError due to: {error.Message}");
        }
    }
}
