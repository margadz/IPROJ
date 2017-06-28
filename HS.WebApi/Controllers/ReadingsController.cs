using System.Collections.Generic;
using System.Threading.Tasks;
using HS.MSSQLRepository.Repository;
using IPROJ.Contracts.DataModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HS.WebApi.Controllers
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
        public Task<IEnumerable<DeviceReading>> GetAllReadings()
        {
            return repository.GetAllReadingsAsync();
        }

        [HttpGet]
        [Route("last")]
        public Task<IEnumerable<DeviceReading>> GetLastReadings()
        {
            return null;
        }
    }
}
