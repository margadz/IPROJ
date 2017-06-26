using System;
using System.Threading;
using Newtonsoft.Json;

namespace CB.Runner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //ReadingsGenerator generator = new ReadingsGenerator(new WebRepository());

            //using (ReadingsMQExchange exchange = new ReadingsMQExchange())
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        var t = Task.Run(() => exchange.Put(MessageGenerator.GetMessage(generator.GenerateReadings())));
            //        t.Wait();
            //        Thread.Sleep(5000);
            //    }
            //}

            TcpMessageBase tcp = new TcpMessageBase("192.168.1.202", 9999);
            var result = tcp.CallTcp(TcpMessageBase.Encrypt(Commands.Emeter));

            var decr = TcpMessageBase.Decrypt(result);

            dynamic conv = JsonConvert.DeserializeObject<dynamic>(decr);

            var emeter = JsonConvert.SerializeObject(conv, Formatting.Indented);

            Console.WriteLine(emeter);
            tcp.Dispose();
            Thread.Sleep(1000);

            Console.ReadKey();
        }
    }
}