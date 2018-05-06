﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Devices.HS110.Commands;
using IPROJ.ConnectionBroker.Devices.HS110.Response;
using IPROJ.ConnectionBroker.Devices.HS110.TcpCommunication;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;
using Newtonsoft.Json;

namespace IPROJ.ConnectionBroker.Devices.HS110
{
    public class HS110Device : Device
    {
        private object _locker = new object();
        private readonly ManualResetEvent _initSync = new ManualResetEvent(false);
        private readonly IHS110TcpConnector _hS110TcpConnector;

        /// <summary>Initializes instance of <see cref="HS110Device"/>/</summary>
        /// <param name="device">Device description.</param>
        /// <param name="logger">Logger.</param>
        public HS110Device(DeviceDescription device, IHS110TcpConnector hS110TcpConnector, IDeviceLog logger) : base (logger)
        {
            Argument.OfWichValueShoulBeProvided(device, nameof(device));
            Argument.OfWichValueShoulBeProvided(hS110TcpConnector, nameof(_hS110TcpConnector));

            _hS110TcpConnector = hS110TcpConnector;
            DeviceId = device.DeviceId;
            Task.Factory.StartNew(EnsureDevice);
        }

        /// <inheritdoc />
        public override Guid DeviceId { get; }

        /// <inheritdoc />
        public override string DeviceName { get; } = "HS110";

        /// <inheritdoc />
        public override ReadingType TypeOfReading { get; } = ReadingType.PowerConsumption;

        public async Task<DeviceReading> GetDailyReading(DateTime date)
        {
            _initSync.WaitOne();

            var response = await _hS110TcpConnector.QueryDevice(CommandStrings.MonthStat(date));
            var result = JsonConvert.DeserializeObject<DailyResponse>(response).emeter.get_daystat.day_list;

            return (from messurement in result
                    where messurement.day == date.Day
                    select new DeviceReading(date, messurement.energy, DeviceId, ReadingType.PowerConsumption, ReadingCharacter.Daily))
                    .FirstOrDefault();
        }

        protected override async Task<DeviceReading> InternalDailyGet()
        {
            var response = await _hS110TcpConnector.QueryDevice(CommandStrings.MonthStat(DateTime.UtcNow));
            var result = JsonConvert.DeserializeObject<DailyResponse>(response).emeter.get_daystat.day_list;

            return (from messurement in result
                    where messurement.day == DateTime.UtcNow.Day
                    select new DeviceReading(DateTime.UtcNow, messurement.energy, DeviceId, ReadingType.PowerConsumption, ReadingCharacter.Daily))
                    .FirstOrDefault();
        }

        protected override async Task<DeviceReading> InternalInstantGet()
        {
            var response = await _hS110TcpConnector.QueryDevice(CommandStrings.Emeter);
            var result = JsonConvert.DeserializeObject<EmeterResponse>(response).emeter.get_realtime.power;
            return new DeviceReading(DateTime.Now, result, DeviceId, ReadingType.PowerConsumption, ReadingCharacter.Instant);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _hS110TcpConnector.Dispose();
        }

        protected override async Task EnsureMethod()
        {
            await _hS110TcpConnector.QueryDevice(CommandStrings.Emeter);
        }
    }
}
