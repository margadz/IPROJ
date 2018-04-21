using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using IPROJ.ConnectionBroker.Autofac;
using IPROJ.ConnectionBroker.Devices.Managing;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.ConnectionBroker.DevicesManager.HS110;
using IPROJ.ConnectionBroker.DevicesManager.Wemo;
using IPROJ.Contracts;
using IPROJ.Contracts.DataModel;

namespace IPROJ.ConntectionBroker.Runner
{
    class Program
    {
        public static TraceSource _source = new TraceSource("ConsoleApp5", SourceLevels.All);
        static void Main(string[] args)
        {
            _source.TraceEvent(TraceEventType.Information, 0, "Found");
            //Lines removed for brevity

            var factory = new ConnectioBrokerFactory();

            var repository = factory.Resolve<IDeviceRepository>();

            var source = new CancellationTokenSource();

            var instant = factory.Resolve<IDeviceManager>();
            var compount = new CompoundDeviceManager(new[] { instant });

            Task.Factory.StartNew(async () => await instant.ManageDevices(source.Token));


            var writer = factory.Resolve<IQueueWriter>();
            var res = new List<DeviceReading>();
            foreach(var dev in repository.Devices)
            {
                res.Add(dev.GetTodaysConsumption().Result);
            }

            writer.Put(res).Wait();

            //Task.Factory.StartNew(async () => await Function(device, writer, source.Token), source.Token);

            //var result = new List<DeviceReading>();
            //var rand = new Random();
            //while (true)
            //{
            //    for (var i = 1; i < 2; i++)
            //    {
            //        result.Add(new DeviceReading(DateTime.Now, new decimal(rand.Next()), Guid.Parse("994FC7B7-9388-43C5-AD09-E16350289785"), ReadingType.PowerComsumption, ReadingCharacter.Instant));
            //    }

            //    writer.Put(result).Wait();
            //    Thread.Sleep(1000);
            //    result.Clear();

            Console.ReadKey();
            source.Cancel();
            source.Dispose();
        }
    }
}
