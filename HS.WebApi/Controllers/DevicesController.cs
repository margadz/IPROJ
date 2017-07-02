using System.Collections.Generic;
using System.Threading.Tasks;
using HS.MSSQLRepository.Repository;
using IPROJ.Contracts.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace HS.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private IDataRepository repository;

        public DevicesController(IDataRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public Task<IEnumerable<Device>> GetAllDevices()
        {
            return repository.GetAllDevicesAsync();
        }
    }
}
