using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CB.Autofac;
using CB.ClientRestApi;
using CB.DevicesManager;
using CB.DevicesManager.HS110;
using CB.DevicesManager.HS110.Commands;
using CB.DevicesManager.HS110.Response;
using IPROJ.Contracts.DataModel;
using IPROJ.QueueManager;
using IPROJ_TcpCommunication;
using Newtonsoft.Json;

namespace CB.Runner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CBFactory factory = new CBFactory();

            var manager = factory.Resolve<IDeviceManager>();
            /*
            HS110TcpConnector connector = new HS110TcpConnector(new TcpConnector(new TcpHost("192.168.1.203:9999")));

            var result = JsonConvert.DeserializeObject<DailyResponse>(connector.QueryDevice(CommandStrings.MonthStat(DateTime.Now)).Result);

            //DeviceReading reading = new DeviceReading();
            //reading.TypeOfReading = "dasdad";

            //manager.Put(new List<DeviceReading>() { reading }).Wait();
            */
            Console.ReadKey();
        }
    }
}
