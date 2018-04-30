using IPROJ.Contracts.Logging;
using Serilog;
using System;

namespace IPROJ.Diagnostics.Serilog
{
    public class QueueLogger : IQueueLogger
    {
        public void InformChannelHasBeenOpened()
        {
            Log.Information("InformChannelHasBeenOpened");
        }

        public void InformQueueServerHasBeenConnected()
        {
            Log.Information("InformQueueServerHasBeenConnected");
        }

        public void RaiseOnQueueServerConnection(Exception error)
        {
            Log.Error($"RaiseOnQueueServerConnection - Cannot connect to Queue Server due to: {error.Message}");
        }
    }
}
