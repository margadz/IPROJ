using Autofac;
using HS.MSSQLRepository.Repository;
using HS.QueueClient;
using IPROJ.Configuration.ConfigurationProvider;
using IPROJ.QueueManager.Connection;
using IPROJ.QueueManager.Messaging;

namespace HS.Autofac
{
    public class HSModule : Module
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
