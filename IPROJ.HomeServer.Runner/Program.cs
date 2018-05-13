using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace IPROJ.HomeServer.Runner
{
    class Program
    {
        public static void Main(string[] args)
        {
            var startup = new HomeServerStartup();
            var tokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(() => startup.Start(tokenSource.Token), tokenSource.Token);

            Console.ReadKey();
            tokenSource.Cancel();
            tokenSource.Dispose();
            startup.Dispose();
        }
    }
}
