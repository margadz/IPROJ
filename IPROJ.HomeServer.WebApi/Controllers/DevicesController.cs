﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.DataRepository;
using IPROJ.HomeServer.WebApi.DataModel;
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

        public DevicesController(IDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [DisableCors]
        public async Task<IEnumerable<DeviceDescription>> GetAllDevices()
        {
            return await _repository.GetAllDevicesAsync();
        }


        [HttpGet("{id}", Name = "id")]
        [DisableCors]
        public async Task<DeviceDescription> GetDevice(string id)
        {
            return (await _repository.GetAllDevicesAsync()).Where(device => device.DeviceId.ToString().ToLower() == id.ToLower()).FirstOrDefault();
        }

        [HttpPost]
        [DisableCors]
        public async Task InsertNewDevice([FromBody]dynamic body)
        {
            string test = body.ToString();
            var jObject = JObject.Parse(test);
            var device = jObject.ToObject<DeviceDescription>().CheckParameters();
            await _repository.AddDeviceAync(device);
        }
    }
}
