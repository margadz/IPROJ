using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Autofac;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.ConnectionBroker.DevicesManager.Wemo;
using IPROJ.Contracts;
using IPROJ.Contracts.DataModel;

namespace IPROJ.ConntectionBroker.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectioBrokerFactory();

            var repository = factory.Resolve<IDeviceRepository>();

            var device = repository.Devices.ToArray();
            var res1 = device[0].GetInsantReading().Result;
            var res2 = device[1].GetInsantReading().Result;

            var writer = factory.Resolve<IQueueWriter>();

            var source = new CancellationTokenSource();

            Task.Factory.StartNew(async () => await Function(device, writer, source.Token), source.Token);
            
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

        private static async Task Function(IDevice[] devices, IQueueWriter writer, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var res1 = await devices[0].GetInsantReading();
                var res2 = await devices[1].GetInsantReading();

                await writer.Put(new[]{ res1, res2 });
                await Task.Delay(1000);
            }
        }
    }
}
