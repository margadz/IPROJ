using System;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Devices;
using IPROJ.ConnectionBroker.Devices.Wemo.HttpCommunication;
using IPROJ.ConnectionBroker.DevicesManager.Wemo.Commands;
using IPROJ.ConnectionBroker.DevicesManager.Wemo.Response;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;

namespace IPROJ.ConnectionBroker.DevicesManager.Wemo
{
    public class WemoDevice : Device
    {
        const string COMMAND_OFF = @"<?xml version=""1.0"" encoding=""utf-8""?><s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:SetBinaryState xmlns:u=""urn:Belkin:service:basicevent:1""><BinaryState>0</BinaryState></u:SetBinaryState></s:Body></s:Envelope>";
        const string COMMAND_ON = @"<?xml version=""1.0"" encoding=""utf-8""?><s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:SetBinaryState xmlns:u=""urn:Belkin:service:basicevent:1""><BinaryState>1</BinaryState></u:SetBinaryState></s:Body></s:Envelope>";
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

        protected override async Task EnsureMethod()
        {
            await _soapCaller.SendRequest(GetInsightParamsWemoCommand.Command);
        }

        protected override async Task<DeviceReading> InternalInstantGet()
        {
            var result = await _soapCaller.SendRequest(GetInsightParamsWemoCommand.Command);
            var response = GetInsightParamsWemoResponse.FromRawResponse(result).InstantReading;
            response.DeviceId = DeviceId;
            return response;
        }

        protected override async Task<DeviceReading> InternalDailyGet()
        {
            var result = await _soapCaller.SendRequest(GetInsightParamsWemoCommand.Command);
            var response = GetInsightParamsWemoResponse.FromRawResponse(result).DailyReading;
            response.DeviceId = DeviceId;
            return response;
        }
    }
}
