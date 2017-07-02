using System;
using System.Threading;
using CB.DevicesManager.HS110;
using IPROJ_TcpCommunication;
using Newtonsoft.Json;

namespace CB.Runner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TcpConnector tcp = new TcpConnector(new TcpHost("192.168.1.202", 9999));
            var result = tcp.CallTcp(HS110Coding.Encrypt(Commands.Emeter));

            var decr = HS110Coding.Decrypt(result.Result);

            dynamic conv = JsonConvert.DeserializeObject<dynamic>(decr);

            var emeter = JsonConvert.SerializeObject(conv, Formatting.Indented);

            Console.WriteLine(emeter);
            tcp.Dispose();
            Thread.Sleep(1000);

            Console.ReadKey();
        }
    }
}