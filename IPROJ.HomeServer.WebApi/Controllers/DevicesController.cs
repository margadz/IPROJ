using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.MSSQLRepository.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IPROJ.HomeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [DisableCors]
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
        [Route("all")]
        [DisableCors]
        public Task<IEnumerable<Device>> GetAllDevices()
        {
            return _repository.GetAllDevicesAsync();
        }


        [HttpGet("id/{id}", Name = "id")]
        [Route("id")]
        public async Task<Device> GetDevice(string id)
        {
            return (await _repository.GetAllDevicesAsync()).Where(device => device.DeviceId.ToString() == id).FirstOrDefault();
        }
    }
}
