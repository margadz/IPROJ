using Autofac;
using IPROJ.Configuration.ConfigurationProvider;
using IPROJ.ConnectionBroker.Devices;
using IPROJ.ConnectionBroker.Devices.HS110.Discovery;
using IPROJ.ConnectionBroker.Devices.Wemo.Discovery;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.ConnectionBroker.Managing;
using IPROJ.ConnectionBroker.Managing.Discovery;
using IPROJ.ConnectionBroker.Managing.Quering;
using IPROJ.ConnectionBroker.Managing.QueueTools;
using IPROJ.Contracts;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Device.Discovery;
using IPROJ.Contracts.Devices;
using IPROJ.Contracts.Logging;
using IPROJ.Contracts.Messaging;
using IPROJ.Diagnostics.Serilog;
using IPROJ.MSSQLRepository.Repository;
using IPROJ.QueueManager.Connection;
using IPROJ.QueueManager.RabbitMQ;
using IPROJ.SignalR.SignalR;
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
            builder.RegisterType<ConfigurationProvider>().As<IConfigurationProvider>().SingleInstance();
            builder.RegisterType<CbConnectionFactory>().As<IRabbitMqConnectionFactory>().SingleInstance();
            builder.RegisterType<DataRepository>()
                .WithParameter(new TypedParameter(typeof(string), @"Data Source=KOMP;Initial Catalog=HomeServer;Integrated Security=True")).As<IDataRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RabbitMqWriter>().As<IQueueWriter>().SingleInstance();
            builder.RegisterType<DeviceRepository>().As<IDeviceRepository>().SingleInstance();
            builder.RegisterType<SignalRMessenger>().As<IMessenger>().SingleInstance();
            builder.RegisterType<DeviceFactory>().As<IDeviceFactory>().SingleInstance();
            builder.RegisterType<DeviceManager>().As<IDeviceManager>().SingleInstance();
            RegisterLoggers(builder);
            RegisterQueries(builder);
            RegisterFinders(builder);
        }

        private static void RegisterLoggers(ContainerBuilder builder)
        {
            builder.RegisterType<DeviceLog>().As<IDeviceLog>().SingleInstance();
            builder.RegisterType<QueueLogger>().As<IQueueLogger>().SingleInstance();
            builder.RegisterType<SignalRMessengerLogger>().As<IInstantMessengerLog>().SingleInstance();
        }

        private static void RegisterQueries(ContainerBuilder builder)
        {
            builder.RegisterType<InstantMessurmentsDeviceQuery>().Named<IDeviceQuery>(nameof(InstantMessurmentsDeviceQuery)).SingleInstance();
            builder.RegisterType<DailyConsumptionDeviceQuery>().Named<IDeviceQuery>(nameof(DailyConsumptionDeviceQuery)).SingleInstance();
            builder
                .Register(ctx => new CompoundDeviceQuery(new[] { ctx.ResolveNamed<IDeviceQuery>(nameof(InstantMessurmentsDeviceQuery)), ctx.ResolveNamed<IDeviceQuery>(nameof(DailyConsumptionDeviceQuery)) }))
                .As<IDeviceQuery>().SingleInstance();

        }

        private static void RegisterFinders(ContainerBuilder builder)
        {
            builder.RegisterType<HS110DeviceFinder>().Named<IDeviceFinder>(nameof(HS110DeviceFinder)).SingleInstance();
            builder.RegisterType<WemoDeviceFinder>().Named<IDeviceFinder>(nameof(WemoDeviceFinder)).SingleInstance();
            builder
                .Register(ctx => new CompoundDeviceFinder(new[] { ctx.ResolveNamed<IDeviceFinder>(nameof(HS110DeviceFinder)), ctx.ResolveNamed<IDeviceFinder>(nameof(WemoDeviceFinder)) }))
                .As<IDeviceFinder>().SingleInstance();
        }
    }
}
