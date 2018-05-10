using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Devices.Wemo.Commands;

namespace IPROJ.ConnectionBroker.Devices.Wemo.HttpCommunication
{
    /// <summary>Describes abstract SoapCaller</summary>
    public interface ISoapCaller
    {
        /// <summary>Send soap commands.</summary>
        /// <param name="command">Command.</param>
        /// <returns>Response.</returns>
        Task<string> SendRequest(IWemoCommand command);
    }
}
