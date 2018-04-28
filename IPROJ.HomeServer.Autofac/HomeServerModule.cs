using Autofac;
using IPROJ.Configuration.ConfigurationProvider;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Logging;
using IPROJ.Contracts.Messaging;
using IPROJ.Diagnostics.Serilog;
using IPROJ.HomeServer.QueueClient;
using IPROJ.HomeServer.SignalR;
using IPROJ.MSSQLRepository.Repository;
using IPROJ.QueueManager.Connection;
using Serilog;

namespace IPROJ.HomeServer.Autofac
{
    public class HomeServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("home_server_log.txt")
                .WriteTo.Console().CreateLogger();

            builder.RegisterType<ConfigurationProvider>().As<IConfigurationProvider>().SingleInstance();
            builder.RegisterType<HsConnectionFactoryProvider>().As<IConnectionFactoryProvider>().SingleInstance();
            builder.RegisterType<QueueListener>().As<IQueueListener>().SingleInstance();
            builder.RegisterType<MessagesHandler>().As<IMessagesHandler>().SingleInstance();
            builder.RegisterType<SignalingDispatcher>().As<ISignalingDispatcher>().SingleInstance();
            builder.RegisterType<DataRepository>().WithParameter(new TypedParameter(typeof(string), @"Data Source=KOMP;Initial Catalog=HomeServer;Integrated Security=True")).As<IDataRepository>().SingleInstance();
            builder.RegisterType<SignalingDispatcherLog>().As<ISignalingDispatcherLog>().SingleInstance();
        }
    }
}
