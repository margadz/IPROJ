using System.Threading.Tasks;

namespace IPROJ.ConnectionBroker.Devices.HS110.TcpCommunication
{
    public class HS110TcpConnector : TcpConnector, IHS110TcpConnector
    {
        public HS110TcpConnector(TcpHost host) : base(host)
        {
        }

        public async Task<string> QueryDevice(string command)
        {
            var response = await CallTcp(HS110Coding.Encrypt(command));

            return HS110Coding.Decrypt(response);
        }
    }
}
