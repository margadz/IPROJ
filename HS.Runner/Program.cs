using HS.Autofac;
using HS.MSSQLRepository.Context;
using System.Linq;

namespace HS.Runner
{
    public class Program
    {
        static void Main(string[] args)
        {

            using (HomeServerDbContext ctx = new HomeServerDbContext())
            {
                var res = (from options in ctx.Devices
                           select options).ToArray();
                          
            }
            //HSModule Factory = new HSModule();

            //HS.WebApi.Program api = new WebApi.Program();
        }
    }
}