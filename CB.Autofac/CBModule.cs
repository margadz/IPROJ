using Autofac;
using CB.ClientRestApi;
using CB.DevicesManager;
using CB.QueueManaging.Exchanges;
using IPROJ.Configuration.ConfigurationProvider;
using IPROJ.QueueManager;
using IPROJ.QueueManager.Connection;

namespace CB.Autofac
{
    public class CBModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationProvider>().As<IConfigurationProvider>().SingleInstance();
            builder.RegisterType<CbConnectionFactoryProvider>().As<IConnectionFactoryProvider>().SingleInstance();
            builder.RegisterType<ReadingsMQExchange>().As<IQueueWriter>().SingleInstance();
            builder.RegisterType<DeviceManager>().As<IDeviceManager>().SingleInstance();
            builder.RegisterType<RestDevicesRepository>().As<IDevicesRepository>().SingleInstance();
        }
    }
}
