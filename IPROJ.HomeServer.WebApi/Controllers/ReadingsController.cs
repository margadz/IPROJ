using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.MSSQLRepository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IPROJ.HomeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ReadingsController : Controller
    {
        private static IDataRepository _repository;

        public ReadingsController()
        {
            if (_repository == null)
            {
                _repository = new DataRepository(@"Data Source=KOMP;Initial Catalog=HomeServer;Integrated Security=True");
            }
        }

        // GET: /<controller>/
        [HttpGet]
        [Route("allreadings")]
        public Task<IEnumerable<DeviceReading>> GetAllReadings()
        {
            return _repository.GetAllReadingsAsync();
        }

        [HttpGet("readingsFor/{id}", Name = "deviceGuid")]
        [Route("readingsFor")]
        public Task<IEnumerable<DeviceReading>> GetReadingsForDevice(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                return _repository.GetAllReadingsFromDeviceAsync(guid);
            }

            return Task.FromResult<IEnumerable<DeviceReading>>(Array.Empty<DeviceReading>());
        }
    }
}
