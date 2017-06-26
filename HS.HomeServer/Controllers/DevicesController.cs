using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HS.Common.OutputModel;
using HS.Common.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HS.HomeServer.Controllers
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
        public Task<List<Device>> GetAllDevices()
        {
            return repository.GetAllDevicesAsync();
        }

        [HttpGet]
        [Route("active")]
        public Task<List<Device>> GetAllActiveDevices()
        {
            return repository.GetAllActiveDevicesAsync();
        }
    }
}
