using IPROJ.Contracts.Logging;
using Serilog;
using System;

namespace IPROJ.Diagnostics.Serilog
{
    /// <summary>Serilog-based implementation of <see cref="IQueueLogger"/>.</summary>
    public class QueueLogger : IQueueLogger
    {
        /// <inheritdoc />
        public void InformChannelHasBeenOpened()
        {
            Log.Information("InformChannelHasBeenOpened");
        }

        /// <inheritdoc />
        public void InformQueueServerHasBeenConnected()
        {
            Log.Information("InformQueueServerHasBeenConnected");
        }

        /// <inheritdoc />
        public void RaiseOnQueueServerConnection(Exception error)
        {
            Log.Error($"RaiseOnQueueServerConnection - Cannot connect to Queue Server due to: {error.Message}");
        }
    }
}
