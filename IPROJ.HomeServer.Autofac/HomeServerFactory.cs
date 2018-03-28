using Autofac;
using IPROJ.Autofac;

namespace IPROJ.HomeServer.Autofac
{
    public class HomeServerFactory : Factory
    {
        protected override void RegisterTypes()
        {
            Builder.RegisterModule(new HomeServerModule());
        }
    }
}