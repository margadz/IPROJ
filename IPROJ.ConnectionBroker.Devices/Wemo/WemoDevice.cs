using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Devices;
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
        private readonly string _url;

        public WemoDevice(DeviceDescription device, IDeviceLog logger) : base (logger)
        {
            Argument.OfWichValueShoulBeProvided(device, nameof(device));

            _url = device.Host;
            DeviceId = device.DeviceId;
            Task.Factory.StartNew(() => EnsureDevice());
        }

        public override Guid DeviceId { get; }

        public override string DeviceName { get; } = "Wemo";

        protected override async Task EnsureMethod()
        {
            await SendRequest(GetInsightParamsWemoCommand.Command);
        }

        protected override async Task<DeviceReading> InternalInstantGet()
        {
            var result = await SendRequest(GetInsightParamsWemoCommand.Command);
            var response = GetInsightParamsWemoResponse.FromRawResponse(result).InstantReading;
            response.DeviceId = DeviceId;
            return response;
        }

        protected override async Task<DeviceReading> InternalDailyGet()
        {
            var result = await SendRequest(GetInsightParamsWemoCommand.Command);
            var response = GetInsightParamsWemoResponse.FromRawResponse(result).DailyReading;
            response.DeviceId = DeviceId;
            return response;
        }

        private async Task<string> SendRequest(IWemoCommand command)
        {
            var url = $"http://{_url}{command.ServiceType}";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("SOAPAction", $"\"{command.SoapAction}\"");
            request.ContentType = @"text/xml; charset=""utf-8""";
            request.KeepAlive = false;
            var payload = Encoding.ASCII.GetBytes(command.Payload);
            request.ContentLength = payload.Length;
            string result;
            using (var stream = await request.GetRequestStreamAsync())
            {
                await stream.WriteAsync(payload, 0, payload.Length);
                var response = await request.GetResponseAsync();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    result = await reader.ReadToEndAsync();
                }
            }
            request.Abort();
            return result;
        }
    }
}
