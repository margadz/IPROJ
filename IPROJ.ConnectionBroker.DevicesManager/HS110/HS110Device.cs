using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.DevicesManager.HS110.Commands;
using IPROJ.ConnectionBroker.TcpCommunication;
using IPROJ.Contracts.DataModel;

namespace IPROJ.ConnectionBroker.DevicesManager.HS110
{
    public class HS110Device : IDevice
    {
        private readonly AutoResetEvent _event = new AutoResetEvent(true);
        private readonly Timer _timer;
        private readonly object _locker = new object();
        private HS110TcpConnector _connector;
        private bool _disposed = false;
        private decimal _currentMessurement;

        public HS110Device(Device device)
        {
            _connector = new HS110TcpConnector(new TcpHost(device.Host));

            if (!EnsureDevice(device).Result)
            {
                throw new DeviceException();
            }

            DeviceId = device.DeviceId;
            TypeOfReading = device.TypeOfReading;
            _timer = new Timer(QueryDevice, null, 0, 1000);
        }

        public Guid DeviceId { get; }

        public ReadingType TypeOfReading { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        public async Task<DeviceReading> GetDailyReading(DateTime date)
        {
            var result = await DailyParser.AquireDailyPowerComsumption(_connector, date);

            return (from messurement in result
                    where messurement.day == date.Day
                    select new DeviceReading(date, messurement.energy, DeviceId, ReadingType.PowerComsumption, ReadingCharacter.Daily))
                    .FirstOrDefault();
        }

        public Task<DeviceReading> GetInsantReading()
        {
            return Task.FromResult(new DeviceReading(DateTime.Now, _currentMessurement, DeviceId, ReadingType.PowerComsumption, ReadingCharacter.Instant));
        }

        private async Task<bool> EnsureDevice(Device device)
        {
            var customId = await SystemInfoParser.AquireSystemInformation(_connector);

            return customId.deviceId == device.CustomId ? true : false;
        }

        private void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _connector?.Dispose();
                _timer?.Dispose();
            }

            _disposed = true;
        }

        private async Task SetCurrentValue()
        {
            _event.WaitOne();
            try
            {
                var value = await EmeterParser.AquireInstantPowerComsumption(_connector);
                _currentMessurement = value;
            }
            catch (SocketException)
            {
                // Supress
            }
            finally
            {
                _event.Set();
            }
        }

        private void QueryDevice(object state)
        {
            SetCurrentValue().Wait();
        }
    }
}
