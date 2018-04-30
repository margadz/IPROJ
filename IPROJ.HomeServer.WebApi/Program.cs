using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace IPROJ.HomeServer.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseIISIntegration()
                .UseKestrel()
                .PreferHostingUrls(true)
                .UseUrls("http://192.168.0.108:12345")
                .Build();
    }
}
