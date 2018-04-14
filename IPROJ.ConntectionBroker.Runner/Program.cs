using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

            //var manager = factory.Resolve<IDeviceManager>();

            //var device = manager.Devices.First();

            var writer = factory.Resolve<IQueueWriter>();

            var result = new List<DeviceReading>();
            var rand = new Random();
            while (true)
            {
                for (var i = 1; i < 2; i++)
                {
                    result.Add(new DeviceReading(DateTime.Now, new decimal(rand.Next()), Guid.Parse("994FC7B7-9388-43C5-AD09-E16350289785"), ReadingType.PowerComsumption, ReadingCharacter.Instant));
                }

                writer.Put(result).Wait();
                Thread.Sleep(1000);
                result.Clear();
            }

            Console.ReadKey();
        }
    }
}
