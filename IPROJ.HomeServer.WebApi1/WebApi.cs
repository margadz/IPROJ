using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace IPROJ.HomeServer.WebApi
{
    public class WebApi
    {
        public WebApi()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls("http://*:12345")
                .UseApplicationInsights()
                .Build();

            host.Run();
        }

    }
}
