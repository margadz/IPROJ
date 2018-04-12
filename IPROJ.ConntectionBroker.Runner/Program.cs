using System;
using System.Collections.Generic;
using System.Linq;
using IPROJ.ConnectionBroker.Autofac;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.Contracts;
using IPROJ.Contracts.DataModel;

namespace IPROJ.ConntectionBroker.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectioBrokerFactory();

            var manager = factory.Resolve<IDeviceManager>();

            var device = manager.Devices.First();

            var writer = factory.Resolve<IQueueWriter>();

            var result = new List<DeviceReading>();
            for (var i = 0; i < 20; i++)
            {
                result.Add(device.GetDailyReading(DateTime.UtcNow.AddDays(-i)).Result);
            }

            writer.Put(result).Wait();

            Console.ReadKey();
        }
    }
}
