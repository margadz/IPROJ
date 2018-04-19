using Autofac;
using IPROJ.Configuration.ConfigurationProvider;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.ConnectionBroker.QueueManaging.Exchanges;
using IPROJ.Contracts;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataRepository;
using IPROJ.MSSQLRepository.Repository;
using IPROJ.QueueManager.Connection;

namespace IPROJ.ConnectionBroker.Autofac
{
    public class ConnectioBrokerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
        }
    }
}
