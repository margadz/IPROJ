using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Messaging;
using IPROJ.HomeServer.Autofac;
using IPROJ.HomeServer.MSSQLRepository;

namespace IPROJ.HomeServer.Runner
{
    public class Program
    {
        private static IDataRepository _repo;

        public static void Main(string[] args)
        {
            HomeServerFactory factory = new HomeServerFactory();

            var listener = factory.Resolve<IQueueListener>();
            _repo = factory.Resolve<IDataRepository>();

            listener.QueueEvent += Fun;

            CancellationTokenSource source;

            source = new CancellationTokenSource();

            Task.Factory.StartNew(() => listener.Listen(source.Token));

            ///HS.WebApi.Program api = new WebApi.Program();

        }

        private static void Fun(IEnumerable<DeviceReading> readings)
        {
            _repo.AddReadingsAsync(readings).Wait();
        }
    }
}