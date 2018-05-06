using System.Collections.Generic;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using Microsoft.AspNetCore.SignalR;

namespace IPROJ.SignalR.Hubs
{
    public class DeviceReadingHub : Hub
    {
        public async Task SendReadings(IEnumerable<DeviceReading> deviceReading)
        {
            await Clients.All.SendAsync("SendReadings", deviceReading);
        }

        public async Task SendDiscoveredDevices(IEnumerable<DeviceDescription> newDevices)
        {
            await Clients.All.SendAsync("SendDiscoveredDevices", newDevices);
        }

        public async Task DiscoverDevicesRequest()
        {
            await Clients.All.SendAsync("DiscoverDevicesRequest");
        }
    }
}
