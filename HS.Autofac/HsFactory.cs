using Autofac;
using IPROJ.Autofac;

namespace HS.Autofac
{
    public class HsFactory : Factory
    {
        protected override void RegisterTypes()
        {
            Builder.RegisterModule(new HSModule());
        }
    }
}