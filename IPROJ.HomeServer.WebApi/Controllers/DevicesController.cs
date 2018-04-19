using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.DataRepository;
using IPROJ.MSSQLRepository.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace IPROJ.HomeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [DisableCors]
    public class DevicesController : Controller
    {
        private static IDataRepository _repository;
        private static bool _run;

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
        public async Task<IEnumerable<DeviceDescription>> GetAllDevices()
        {
            return await _repository.GetAllDevicesAsync();
        }


        [HttpGet("id/{id}", Name = "id")]
        [Route("id")]
        public async Task<DeviceDescription> GetDevice(string id)
        {
            return (await _repository.GetAllDevicesAsync()).Where(device => device.DeviceId.ToString() == id).FirstOrDefault();
        }
    }
}
