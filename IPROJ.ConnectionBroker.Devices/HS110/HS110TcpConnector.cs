using System.Threading.Tasks;
using IPROJ.ConnectionBroker.TcpCommunication;

namespace IPROJ.ConnectionBroker.DevicesManager.HS110
{
    public class HS110TcpConnector : TcpConnector
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
