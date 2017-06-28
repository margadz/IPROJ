using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using HS.MSSQLRepository.Context.Repository;
using HS.MSSQLRepository.Repository;

namespace HS.Autofac
{
    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DummRepository>().As<IDataRepository>().InstancePerLifetimeScope();
        }
    }
}
