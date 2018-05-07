using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Devices;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Threading;

namespace IPROJ.ConnectionBroker.DevicesManager
{
    public sealed class DeviceRepository : IDeviceRepository
    {
        private readonly ManualResetEvent _syncEvent = new ManualResetEvent(false);
        private readonly IDataRepository _dataRepository;
        private readonly IDeviceFactory _deviceFactory;
        private readonly CancellationToken _cancellationToken;
        private IEnumerable<IDevice> _devices;

        public DeviceRepository(IDataRepository dataRepository, IDeviceFactory deviceFactory, IThreadingInfrastructure threadingInfrastructure)
        {
            Argument.OfWichValueShoulBeProvided(dataRepository, nameof(dataRepository));
            Argument.OfWichValueShoulBeProvided(deviceFactory, nameof(deviceFactory));
            Argument.OfWichValueShoulBeProvided(threadingInfrastructure, nameof(threadingInfrastructure));

            _dataRepository = dataRepository;
            _deviceFactory = deviceFactory;
            _cancellationToken = threadingInfrastructure.CancellationToken;
            Task.Run(CollectDevices, _cancellationToken);
            Task.Run(ReloadDevices, _cancellationToken);
        }

        public IEnumerable<IDevice> Devices
        {
            get
            {
                _syncEvent.WaitOne();
                return _devices;
            }
        }

        public void Dispose()
        {
            DisposeDevices();

            _syncEvent.Dispose();
            GC.SuppressFinalize(this);
        }

        private void DisposeDevices()
        {
            if (_devices == null || !_devices.Any())
            {
                return;
            }

            foreach (var device in _devices)
            {
                device?.Dispose();
            }
        }

        private async Task CollectDevices()
        {
            DisposeDevices();
            var rawDevices = await _dataRepository.GetAllDevicesAsync();
            var result = new List<IDevice>();

            foreach (var dev in rawDevices)
            {
                result.Add(_deviceFactory.CreateDevice(dev));
            }

            _devices = result;
            _syncEvent.Set();
        }

        private async Task ReloadDevices()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                _syncEvent.WaitOne();
                _syncEvent.Reset();
                var newDevices = await _dataRepository.GetAllDevicesAsync();
                if (newDevices.Count() != _devices.Count())
                {
                    await CollectDevices();
                }
                _syncEvent.Set();
                await Task.Delay(2000);
            }
        }
    }
}
