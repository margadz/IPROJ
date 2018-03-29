using Autofac;
using IPROJ.Configuration.ConfigurationProvider;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.Messaging;
using IPROJ.HomeServer.QueueClient;
using IPROJ.MSSQLRepository.Repository;
using IPROJ.QueueManager.Connection;

namespace IPROJ.HomeServer.Autofac
{
    public class HomeServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationProvider>().As<IConfigurationProvider>().SingleInstance();
            builder.RegisterType<HsConnectionFactoryProvider>().As<IConnectionFactoryProvider>().SingleInstance();
            builder.RegisterType<QueueListener>().As<IQueueListener>().SingleInstance();
            builder.RegisterType<DataRepository>().WithParameter(new TypedParameter(typeof(string), @"Data Source=KOMP;Initial Catalog=HomeServer;Integrated Security=True")).As<IDataRepository>().InstancePerLifetimeScope();
        }
    }
}
