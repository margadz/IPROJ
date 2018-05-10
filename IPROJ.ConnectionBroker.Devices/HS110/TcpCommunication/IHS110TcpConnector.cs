using System;
using System.Threading.Tasks;

namespace IPROJ.ConnectionBroker.Devices.HS110.TcpCommunication
{
    /// <summary>Describes abstract HS110 connector</summary>
    public interface IHS110TcpConnector : IDisposable
    {
        Task<string> QueryDevice(string command);
    }
}