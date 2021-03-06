﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IPROJ.Contracts.Logging
{
    public interface IInstantMessengerLogger
    {
        void InformDispacherIsConnectingToHub();

        void RaiseDispatcherHubConnectionError(Exception error);

        void InformDispatcherConnectedToHub();
    }
}
