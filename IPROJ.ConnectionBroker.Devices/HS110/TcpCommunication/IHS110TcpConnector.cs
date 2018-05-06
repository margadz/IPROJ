using System;
using System.Threading.Tasks;

namespace IPROJ.ConnectionBroker.Devices.HS110.TcpCommunication
{
    /// <summary>Describes abstract H</summary>
    public interface IHS110TcpConnector : IDisposable
    {
        Task<string> QueryDevice(string command);
    }
}