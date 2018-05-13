using System;
using IPROJ.Contracts.Logging;
using Serilog;

namespace IPROJ.Diagnostics.Serilog
{
    public class StartupLogger : IStartupLogger
    {
        public void InformStartupProcessHasStarted(string name)
        {
            Log.Information($"InformStartupProcessHasStarted: {name}");
        }

        public void InformStartupProcessIsStarting(string name)
        {
            Log.Information($"InformStartupProcessIsStarting: {name}");
        }

        public void RaiseOnErrorDuringStartupProcessStart(Exception error, string name)
        {
            Log.Error($"RaiseOnErrorDuringStartupProcessStart: startup of {name} failed due to: {error.Message}");
        }
    }
}
