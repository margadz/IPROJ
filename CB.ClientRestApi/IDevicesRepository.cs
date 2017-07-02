using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;

namespace CB.ClientRestApi
{
    public interface IDevicesRepository
    {
        Task<IEnumerable<Device>> GetAllDevicesAsync();
    }
}