using System;
using Autofac;
using IPROJ.Configuration;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.Threading;

namespace IPROJ.Autofac
{
    public abstract class Factory : IDisposable
    {
        private ILifetimeScope _scope;

        public Factory()
        {
            if (Container == null)
            {
                Builder = new ContainerBuilder();
                Builder.RegisterType<ConfigurationProvider>().As<IConfigurationProvider>().SingleInstance();
                Builder.RegisterType<ThreadingInfrastructure>().As<IThreadingInfrastructure>().SingleInstance();
                RegisterTypes();
                Container = Builder.Build();
            }

            _scope = Container.BeginLifetimeScope();
        }

        protected static IContainer Container { get; set; }

        protected ContainerBuilder Builder { get; }

        public void Dispose()
        {
            _scope?.Dispose();
            Container?.Dispose();
        }

        public T Resolve<T>()
        {
            return _scope.Resolve<T>();
        }

        protected abstract void RegisterTypes();
    }
}
