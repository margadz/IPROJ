using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using Microsoft.AspNetCore.SignalR;

namespace IPROJ.SignalR.Hubs
{
    public class DeviceReadingHub : Hub
    {
        public async Task SendMessage(IEnumerable<DeviceReading> deviceReading)
        {
            await Clients.All.SendAsync("SendMessage", deviceReading);
            Console.WriteLine($"Instant readings sent.");
        }
    }
}
