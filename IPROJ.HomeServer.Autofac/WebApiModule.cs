﻿using Autofac;
using IPROJ.Contracts.DataRepository;
using IPROJ.MSSQLRepository.Repository;

namespace IPROJ.HomeServer.Autofac
{
    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataRepository>().WithParameter(new TypedParameter(typeof(string), @"Data Source=KOMP;Initial Catalog=HomeServer;Integrated Security=True")).As<IDataRepository>().InstancePerLifetimeScope();
        }
    }
}
