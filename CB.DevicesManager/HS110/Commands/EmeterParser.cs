using System.Threading.Tasks;
using CB.DevicesManager.HS110.Response;
using Newtonsoft.Json;

namespace CB.DevicesManager.HS110.Commands
{
    public static class EmeterParser
    {
        public static async Task<decimal> AquireInstantPowerComsumption(HS110TcpConnector connector)
        {
            var response = await connector.QueryDevice(CommandStrings.Emeter);

            return JsonConvert.DeserializeObject<EmeterResponse>(response).emeter.get_realtime.power;
        }
    }
}
