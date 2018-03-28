using Autofac;
using IPROJ.Autofac;

namespace IPROJ.ConnectionBroker.Autofac
{
    public class CBFactory : Factory
    {
        protected override void RegisterTypes()
        {
            Builder.RegisterModule(new CBModule());
        }
    }
}
