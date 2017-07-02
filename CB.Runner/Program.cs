using System;
using CB.DevicesManager.HS110;
using IPROJ.Contracts.DataModel;

namespace CB.Runner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //TcpConnector tcp = new TcpConnector(new TcpHost("192.168.1.202", 9999));

            //var result = tcp.CallTcp(HS110Coding.Encrypt(Comman.SysInfo)).Result;

            //var decr = HS110Coding.Decrypt(result);

            //var conv = JsonConvert.DeserializeObject<SystemResponse>(decr);

            //var emeter = JsonConvert.SerializeObject(conv, Formatting.Indented);

            //Console.WriteLine(emeter);
            //tcp.Dispose();

            //SystemInformation info = new SystemInformation() { active_mode = "das", err_code = "1", rssi = "da", longitude = "das" };

            //SystemCommand commane = new SystemCommand() { get_sysinfo = info };

            //var dec = JsonConvert.SerializeObject(commane, Formatting.Indented);

            Device dev = new Device() { CustomId = "8006D1847073EC74595FFCD43771CB2817AFBCAD", Host = "192.168.1.202:9999" };

            HS110Device device = new HS110Device(dev);
 

            Console.ReadKey();
        }
    }
}
