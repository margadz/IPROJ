using HS.Common.Interfaces;
using HS.Common.OutputModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HS.HomeServer.Controllers
{
    [Route("api/[controller]")]
    public class ReadingsController : Controller
    {
        private IDataRepository repository;

        public ReadingsController(IDataRepository repository)
        {
            this.repository = repository;
        }

        // GET: /<controller>/
        [HttpGet]
        public Task<List<DeviceReading>> GetAllReadings()
        {
            return repository.GetAllReadingsAsync();
        }

        [HttpGet]
        [Route("last")]
        public Task<List<DeviceReading>> GetLastReadings()
        {
            return repository.GetLastReadingsAsync();
        }
    }
}
