using System;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Devices;
using IPROJ.ConnectionBroker.Devices.Wemo.Commands;
using IPROJ.ConnectionBroker.Devices.Wemo.HttpCommunication;
using IPROJ.ConnectionBroker.DevicesManager.Wemo.Response;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;

namespace IPROJ.ConnectionBroker.DevicesManager.Wemo
{
    public class WemoDevice : Device
    {
        private readonly ISoapCaller _soapCaller;

        public WemoDevice(DeviceDescription device, ISoapCaller soapCaller,  IDeviceLogger logger) : base (logger)
        {
            Argument.OfWichValueShoulBeProvided(device, nameof(device));
            Argument.OfWichValueShoulBeProvided(soapCaller, nameof(soapCaller));

            _soapCaller = soapCaller;
            DeviceId = device.DeviceId;
            Task.Factory.StartNew(EnsureDevice);
        }

        /// <inheritdoc />
        public override Guid DeviceId { get; }

        /// <inheritdoc />
        public override string DeviceName { get; } = "Wemo";

        /// <inheritdoc />
        public override ReadingType TypeOfReading { get; } = ReadingType.PowerConsumption;

        /// <inheritdoc />
        public override async Task SetState(DeviceState deviceState)
        {
            await _soapCaller.SendRequest(deviceState == DeviceState.On ? SetInsightStateCommand.On : SetInsightStateCommand.Off);
        }

        protected override async Task EnsureMethod()
        {
            await _soapCaller.SendRequest(GetInsightParamsWemoCommand.Command);
        }

        protected override async Task<DeviceReading> InternalInstantGet()
        {
            var result = await _soapCaller.SendRequest(GetInsightParamsWemoCommand.Command);
            var response = InsightParamsWemoParser.FromRawResponse(result).InstantReading;
            response.DeviceId = DeviceId;
            return response;
        }

        protected override async Task<DeviceReading> InternalDailyGet()
        {
            var result = await _soapCaller.SendRequest(GetInsightParamsWemoCommand.Command);
            var response = InsightParamsWemoParser.FromRawResponse(result).DailyReading;
            response.DeviceId = DeviceId;
            return response;
        }
    }
}
