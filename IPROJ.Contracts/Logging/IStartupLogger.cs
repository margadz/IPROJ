using System;

namespace IPROJ.Contracts.Logging
{
    public interface IStartupLogger
    {
        void InformStartupProcessIsStarting(string name);

        void InformStartupProcessHasStarted(string named);

        void RaiseOnErrorDuringStartupProcessStart(Exception error, string name);
    }
}
