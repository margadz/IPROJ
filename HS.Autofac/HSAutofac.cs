using System;
using Autofac;
using IPROJ.Autofac;
using IPROJ.Configuration.ConfigurationProvider;

namespace HS.Autofac
{
    public class HSModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationProvider>().As<IConfigurationProvider>().InstancePerLifetimeScope();
        }
    }
}
