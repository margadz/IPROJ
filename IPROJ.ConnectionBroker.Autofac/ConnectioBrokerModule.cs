using System.Collections.Generic;
using Autofac;
using IPROJ.Configuration.ConfigurationProvider;
using IPROJ.ConnectionBroker.Devices.Managing;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.ConnectionBroker.QueueManaging.Exchanges;
using IPROJ.Contracts;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Logging;
using IPROJ.Diagnostics.Serilog;
using IPROJ.MSSQLRepository.Repository;
using IPROJ.QueueManager.Connection;
using Serilog;

namespace IPROJ.ConnectionBroker.Autofac
{
    public class ConnectioBrokerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("connection_broker_log.txt")
                .WriteTo.Console().CreateLogger();

            base.Load(builder);
            builder.RegisterType<ConfigurationProvider>()
                   .As<IConfigurationProvider>()
                   .SingleInstance();

            builder.RegisterType<CbConnectionFactoryProvider>()
                   .As<IConnectionFactoryProvider>()
                   .SingleInstance();

            builder.RegisterType<DataRepository>().WithParameter(new TypedParameter(typeof(string), @"Data Source=KOMP;Initial Catalog=HomeServer;Integrated Security=True")).As<IDataRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ReadingsMQExchange>()
                   .As<IQueueWriter>()
                   .SingleInstance();
            builder.RegisterType<DeviceRepository>().As<IDeviceRepository>().SingleInstance();
            builder.RegisterType<InstantMessurmentsDeviceManager>().As<IDeviceManager>().SingleInstance();
            builder.RegisterType<DailyConsumptionDeviceManager>().As<IDeviceManager>().SingleInstance();

            RegisterLoggers(builder);
        }

        private static void RegisterLoggers(ContainerBuilder builder)
        {
            builder.RegisterType<DeviceLog>().As<IDeviceLog>().SingleInstance();
        }
    }
}
