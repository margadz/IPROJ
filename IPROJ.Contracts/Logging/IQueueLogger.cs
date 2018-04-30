using System;

namespace IPROJ.Contracts.Logging
{
    public interface IQueueLogger
    {
        void InformQueueServerHasBeenConnected();

        void RaiseOnQueueServerConnection(Exception error);

        void InformChannelHasBeenOpened();
    }
}
