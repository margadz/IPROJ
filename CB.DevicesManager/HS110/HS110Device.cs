using System;
using System.Threading.Tasks;
using CB.DevicesManager.HS110.Commands;
using IPROJ.Contracts.DataModel;
using IPROJ_TcpCommunication;

namespace CB.DevicesManager.HS110
{
    public class HS110Device : IDevice
    {
        private HS110TcpConnector _connector;
        private bool _disposed = false;

        public HS110Device(Device device)
        {
            _connector = new HS110TcpConnector(new TcpConnector(new TcpHost(device.Host)));

            if (!EnsureDevice(device).Result)
            {
                throw new ArgumentOutOfRangeException();
            }

            DeviceId = device.DeviceId;
        }

        public Guid DeviceId { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        public Task<DeviceReading> GetDailyReading()
        {
            throw new NotImplementedException();
        }

        public Task<DeviceReading> GetInsantReading()
        {
            throw new NotImplementedException();
        }

        private async Task<bool> EnsureDevice(Device device)
        {
            var customId = await SystemInfoCommand.AquireSystemInformation(_connector);

            return customId.deviceId == device.CustomId ? true : false;
        }

        private void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _connector?.Dispose();
            }

            _disposed = true;
        }
    }
}
