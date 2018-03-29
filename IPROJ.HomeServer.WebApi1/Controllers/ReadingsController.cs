using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.HomeServer.MSSQLRepository;
using Microsoft.AspNetCore.Mvc;

namespace IPROJ.HomeServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ReadingsController : Controller
    {
        private static IDataRepository _repository;

        public ReadingsController(IDataRepository repository)
        {
            if (_repository == null)
            {
                _repository = new DataRepository(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=HomeServer;Integrated Security=True");
            }
        }

        // GET: /<controller>/
        [HttpGet]
        [Route("allreadings")]
        public Task<IEnumerable<DeviceReading>> GetAllReadings()
        {
            return _repository.GetAllReadingsAsync();
        }

        [HttpGet("{id}", Name = "deviceGuid")]
        [Route("readingFor")]
        public Task<IEnumerable<DeviceReading>> GetReadingsForDevice(string deviceGuid)
        {
            Guid guid;
            if (Guid.TryParse(deviceGuid, out guid))
            {
                return _repository.GetAllReadingsFromDeviceAsync(guid);
            }

            return Task.FromResult<IEnumerable<DeviceReading>>(Array.Empty<DeviceReading>());
        }
    }
}
