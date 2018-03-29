using System.Collections.Generic;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.MSSQLRepository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IPROJ.HomeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private static IDataRepository _repository;

        public DevicesController()
        {
            if (_repository == null)
            {
                _repository = new DataRepository(@"Data Source=KOMP;Initial Catalog=HomeServer;Integrated Security=True");
            }
        }

        [HttpGet]
        public Task<IEnumerable<Device>> GetAllDevices()
        {
            return _repository.GetAllDevicesAsync();
        }
    }
}
