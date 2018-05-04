using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.DataRepository;
using IPROJ.HomeServer.WebApi.DataModel;
using IPROJ.MSSQLRepository.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
                _repository = new DataRepository(@"Data Source=LAPSAM;Initial Catalog=HomeServer;Integrated Security=True");
            }
        }

        [HttpGet]
        [Route("all")]
        [DisableCors]
        public async Task<IEnumerable<DeviceDescription>> GetAllDevices()
        {
            return await _repository.GetAllDevicesAsync();
        }


        [HttpGet("id/{id}", Name = "id")]
        [Route("id")]
        public async Task<DeviceDescription> GetDevice(string id)
        {
            return (await _repository.GetAllDevicesAsync()).Where(device => device.DeviceId.ToString().ToLower() == id.ToLower()).FirstOrDefault();
        }

        [HttpPost]
        [Route("add")]
        public async Task InsertNewDevice([FromBody]dynamic body)
        {
            string test = body.ToString();
            var jObject = JObject.Parse(test);
            var device = jObject.ToObject<DeviceDescription>().CheckParameters();
            await _repository.AddDeviceAync(device);
        }
    }
}
