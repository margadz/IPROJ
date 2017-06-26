using CB.Common.Interfaces;
using CB.Common.Model;
using System.Collections.Generic;

namespace CB.DataHandling
{
    public class DataHandler
    {
        private IDevicesRepository _deviceRepository;
        private List<Device> _devices;

        public DataHandler(IDevicesRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public IList<Device> Devices
        {
            get
            {
                if (_devices == null)
                {
                    _devices = (List<Device>)_deviceRepository.GetAllActiveDevicesAsync().Result;
                }
                return _devices;
            }
        }
    }
}
