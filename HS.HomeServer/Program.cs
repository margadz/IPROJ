using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using HS.MSSQLRepository.Repository;
using HS.QueueClient;

namespace HS.HomeServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls("http://*:12345")
                .UseApplicationInsights()
                .Build();

            QueueListener listner = new QueueListener(new MSSQLDataRepository());
            Task.Factory.StartNew(() => listner.Run() );

            host.Run();
        }

    }
}
