using Autofac;
using IPROJ.Autofac;

namespace CB.Autofac
{
    public class CBFactory : Factory
    {
        protected override void RegisterTypes()
        {
            Builder.RegisterModule(new CBModule());
        }
    }
}
