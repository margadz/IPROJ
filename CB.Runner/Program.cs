using System;
using System.Linq;
using System.Threading;
using CB.ClientRestApi;
using CB.DevicesManager.HS110;
using CB.DevicesManager.HS110.Commands;
using CB.DevicesManager.HS110.Response;
using IPROJ.Contracts.DataModel;
using IPROJ_TcpCommunication;
using Newtonsoft.Json;

namespace CB.Runner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RestDevicesRepository api = new RestDevicesRepository();

            var es = api.GetAllDevicesAsync().Result.ToArray();

            Console.ReadKey();
        }
    }
}
