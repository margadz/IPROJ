using System.Threading.Tasks;
using CB.DevicesManager.HS110.Response;
using Newtonsoft.Json;

namespace CB.DevicesManager.HS110.Commands
{
    public static class SystemInfoCommand
    {
        public static async Task<SystemInformation> AquireSystemInformation(HS110TcpConnector connector)
        {
            var response = await connector.QueryDevice(Comman.SysInfo);

            return JsonConvert.DeserializeObject<SystemResponse>(response).System.get_sysinfo;
        }
    }
}
