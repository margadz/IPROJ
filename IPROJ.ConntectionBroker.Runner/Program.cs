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

            var instant = factory.Resolve<IEnumerable<IDeviceManager>>();
            var compount = new CompoundDeviceManager(instant);

            Task.Factory.StartNew(async () => await compount.ManageDevices(source.Token));

            Console.ReadKey();
            source.Cancel();
            source.Dispose();
        }
    }
}
