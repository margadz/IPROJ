using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.DataRepository;
using Microsoft.AspNetCore.Mvc;

namespace IPROJ.HomeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ReadingsController : Controller
    {
        private IDataRepository _repository;

        public ReadingsController(IDataRepository repository)
        {
            _repository = repository;
        }

        // GET: /<controller>/
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<DeviceReading>> GetAllReadings()
        {
            return await _repository.GetAllReadingsAsync();
        }

        [HttpGet("{id}", Name = "deviceGuid")]
        [Route("")]
        public async Task<IEnumerable<DeviceReading>> GetReadingsForDevice(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                return await _repository.GetAllReadingsFromDeviceAsync(guid);
            }

            return await Task.FromResult<IEnumerable<DeviceReading>>(Array.Empty<DeviceReading>());
        }


    }
}
