using System;
using System.Linq;
using IPROJ.ConnectionBroker.Autofac;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.ConnectionBroker.DevicesManager.HS110;

namespace IPROJ.ConntectionBroker.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectioBrokerFactory();

            var manager = factory.Resolve<IDeviceManager>();

            var device = manager.Devices.First();

            var res1 = device.GetDailyReading(DateTime.UtcNow).Result;
            var res2 = device.GetDailyReading(DateTime.UtcNow.AddDays(-1)).Result;

            Console.ReadKey();
        }
    }
}
