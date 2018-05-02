using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Devices;
using IPROJ.ConnectionBroker.DevicesManager.HS110.Commands;
using IPROJ.ConnectionBroker.DevicesManager.HS110.Response;
using IPROJ.ConnectionBroker.TcpCommunication;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;
using Newtonsoft.Json;

namespace IPROJ.ConnectionBroker.DevicesManager.HS110
{
    public class HS110Device : Device
    {
        private object _locker = new object();
        private readonly HS110TcpConnector _connector;
        private readonly ManualResetEvent _initSync = new ManualResetEvent(false);


        public HS110Device(DeviceDescription device, IDeviceLog logger) : base (logger)
        {
            Argument.OfWichValueShoulBeProvided(device, nameof(device));

            _connector = new HS110TcpConnector(new TcpHost(device.Host));
            DeviceId = device.DeviceId;
            Task.Factory.StartNew(EnsureDevice);
        }

        public override Guid DeviceId { get; }

        public override string DeviceName { get; } = "HS110";

        public ReadingType TypeOfReading { get; } = ReadingType.PowerComsumption;

        public async Task<DeviceReading> GetDailyReading(DateTime date)
        {
            _initSync.WaitOne();

            var response = await _connector.QueryDevice(CommandStrings.MonthStat(date));
            var result = JsonConvert.DeserializeObject<DailyResponse>(response).emeter.get_daystat.day_list;

            return (from messurement in result
                    where messurement.day == date.Day
                    select new DeviceReading(date, messurement.energy, DeviceId, ReadingType.PowerComsumption, ReadingCharacter.Daily))
                    .FirstOrDefault();
        }

        protected override async Task<DeviceReading> InternalDailyGet()
        {
            var response = await _connector.QueryDevice(CommandStrings.MonthStat(DateTime.UtcNow));
            var result = JsonConvert.DeserializeObject<DailyResponse>(response).emeter.get_daystat.day_list;

            return (from messurement in result
                    where messurement.day == DateTime.UtcNow.Day
                    select new DeviceReading(DateTime.UtcNow, messurement.energy, DeviceId, ReadingType.PowerComsumption, ReadingCharacter.Daily))
                    .FirstOrDefault();
        }

        protected override async Task<DeviceReading> InternalInstantGet()
        {
            var response = await _connector.QueryDevice(CommandStrings.Emeter);
            var result = JsonConvert.DeserializeObject<EmeterResponse>(response).emeter.get_realtime.power;
            return new DeviceReading(DateTime.Now, result, DeviceId, ReadingType.PowerComsumption, ReadingCharacter.Instant);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connector.Dispose();
        }

        protected override async Task EnsureMethod()
        {
            await _connector.QueryDevice(CommandStrings.Emeter);
        }
    }
}
