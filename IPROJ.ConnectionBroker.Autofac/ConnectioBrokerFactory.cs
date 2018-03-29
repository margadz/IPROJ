using Autofac;
using IPROJ.Autofac;

namespace IPROJ.ConnectionBroker.Autofac
{
    public class ConnectioBrokerFactory : Factory
    {
        protected override void RegisterTypes()
        {
            Builder.RegisterModule(new ConnectioBrokerModule());
        }
    }
}
