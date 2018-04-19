using System;
using System.Linq;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.DevicesManager.HS110.Commands;
using IPROJ.ConnectionBroker.DevicesManager.HS110.Response;
using IPROJ.ConnectionBroker.TcpCommunication;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using Newtonsoft.Json;

namespace IPROJ.ConnectionBroker.DevicesManager.HS110
{
    public class HS110Device : IDevice
    {
        private readonly string _deviceId;
        private HS110TcpConnector _connector;
        private bool _disposed = false;

        public HS110Device(DeviceDescription device)
        {
            Argument.OfWichValueShoulBeProvided(device, nameof(device));

            _connector = new HS110TcpConnector(new TcpHost(device.Host));
            _deviceId = device.CustomId;
            DeviceId = device.DeviceId;
        }

        public Guid DeviceId { get; }

        public string DeviceName { get; } = "HS110";

        public ReadingType TypeOfReading { get; } = ReadingType.PowerComsumption;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        public async Task<DeviceReading> GetTodaysConsumption()
        {
            return await GetDailyReading(DateTime.Today);
        }

        public async Task<DeviceReading> GetDailyReading(DateTime date)
        {
            if (!(await EnsureDevice()))
            {
                return null;
            }

            var response = await _connector.QueryDevice(CommandStrings.MonthStat(date));
            var result = JsonConvert.DeserializeObject<DailyResponse>(response).emeter.get_daystat.day_list;

            return (from messurement in result
                    where messurement.day == date.Day
                    select new DeviceReading(date, messurement.energy, DeviceId, ReadingType.PowerComsumption, ReadingCharacter.Daily))
                    .FirstOrDefault();
        }

        public async Task<DeviceReading> GetInsantReading()
        {
            if (!(await EnsureDevice()))
            {
                return null;
            }

            var response = await _connector.QueryDevice(CommandStrings.Emeter);
            var result = JsonConvert.DeserializeObject<EmeterResponse>(response).emeter.get_realtime.power;
            return new DeviceReading(DateTime.Now, result, DeviceId, ReadingType.PowerComsumption, ReadingCharacter.Instant);
        }

        private async Task<bool> EnsureDevice()
        {
            var customId = await SystemInfoParser.AquireSystemInformation(_connector);

            return customId.deviceId == _deviceId ? true : false;
        }

        private void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _connector.Dispose();
            }

            _disposed = true;
        }
    }
}
