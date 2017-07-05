using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HS.Autofac;
using HS.MSSQLRepository.Repository;
using IPROJ.Contracts.DataModel;
using IPROJ.QueueManager.Messaging;

namespace HS.Runner
{
    public class Program
    {
        private static IDataRepository _repo;

        public static void Main(string[] args)
        {
            HsFactory factory = new HsFactory();

            var listener = factory.Resolve<IQueueListener>();
            _repo = factory.Resolve<IDataRepository>();

            listener.QueueEvent += Fun;

            CancellationTokenSource source;

            source = new CancellationTokenSource();

            Task.Factory.StartNew(() => listener.Listen(source.Token));

            HS.WebApi.Program api = new WebApi.Program();

        }

        private static void Fun (IEnumerable<DeviceReading> readings)
        {
            _repo.AddReadingsAsync(readings).Wait();
        }
    }
}