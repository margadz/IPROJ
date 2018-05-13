using Autofac;
using IPROJ.Configuration.ConfigurationProvider;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Logging;
using IPROJ.Contracts.Messaging;
using IPROJ.Contracts.Threading;
using IPROJ.Diagnostics.Serilog;
using IPROJ.HomeServer.QueueClient;
using IPROJ.MSSQLRepository.Repository;
using IPROJ.QueueManager.Connection;
using IPROJ.QueueManager.RabbitMQ;
using Microsoft.IdentityModel.Protocols;
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
            builder.RegisterType<HsConnectionFactory>().As<IRabbitMqConnectionFactory>().SingleInstance();
            builder.RegisterType<RabbitMqListener>().As<IQueueListener>().SingleInstance();
            builder.RegisterType<MessagesHandler>().As<IMessagesHandler>().SingleInstance();
            builder.RegisterType<DataRepository>().WithParameter(new TypedParameter(typeof(string), @"Data Source=KOMP;Initial Catalog=HomeServer;Integrated Security=True")).As<IDataRepository>().SingleInstance();
            builder.RegisterType<ThreadingInfrastructure>().As<IThreadingInfrastructure>().SingleInstance();
            RegisterLoggers(builder);
        }

        private static void RegisterLoggers(ContainerBuilder builder)
        {
            builder.RegisterType<QueueLogger>().As<IQueueLogger>().SingleInstance();
        }
    }
}
