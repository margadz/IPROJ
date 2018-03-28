using System.Threading.Tasks;
using IPROJ.ConnectionBroker.DevicesManager.HS110.Response;
using Newtonsoft.Json;

namespace IPROJ.ConnectionBroker.DevicesManager.HS110.Commands
{
    public static class SystemInfoParser
    {
        public static async Task<SystemInformation> AquireSystemInformation(HS110TcpConnector connector)
        {
            var response = await connector.QueryDevice(CommandStrings.SysInfo);

            return JsonConvert.DeserializeObject<SystemResponse>(response).System.get_sysinfo;
        }
    }
}
