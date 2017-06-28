using HS.Autofac;

namespace HS.Runner
{
    public class Program
    {
        static void Main(string[] args)
        {
            HSAutofac Factory = new HSAutofac();

            HS.WebApi.Program api = new WebApi.Program();
        }
    }
}