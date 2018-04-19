using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.DevicesManager.Wemo.Commands;
using IPROJ.ConnectionBroker.DevicesManager.Wemo.Response;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;

namespace IPROJ.ConnectionBroker.DevicesManager.Wemo
{
    public class WemoDevice : IDevice
    {
        const string COMMAND_OFF = @"<?xml version=""1.0"" encoding=""utf-8""?><s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:SetBinaryState xmlns:u=""urn:Belkin:service:basicevent:1""><BinaryState>0</BinaryState></u:SetBinaryState></s:Body></s:Envelope>";
        const string COMMAND_ON = @"<?xml version=""1.0"" encoding=""utf-8""?><s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"" s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><s:Body><u:SetBinaryState xmlns:u=""urn:Belkin:service:basicevent:1""><BinaryState>1</BinaryState></u:SetBinaryState></s:Body></s:Envelope>";
        private readonly string _url;

        public WemoDevice(DeviceDescription device)
        {
            Argument.OfWichValueShoulBeProvided(device, nameof(device));
            _url = device.Host;
            DeviceId = device.DeviceId;
        }

        public Guid DeviceId { get; }

        public string DeviceName { get; } = "Wemo";

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<DeviceReading> GetTodaysConsumption()
        {
            throw new NotImplementedException();
        }

        public async Task<DeviceReading> GetInsantReading()
        {
            var result = await SendRequest(GetInsightParamsWemoCommand.Command);
            var response = GetInsightParamsWemoResponse.FromRawResponse(result).InstantReading;
            response.DeviceId = DeviceId;
            return response;
        }

        //public async Task On()
        //{
        //    var targetUrl = $"http://{_url}/upnp/control/basicevent1";
        //    var res = await SendRequest(targetUrl, COMMAND_ON);
        //}

        //public async Task Off()
        //{
        //    var targetUrl = $"http://{_url}/upnp/control/basicevent1";
        //    var res = await SendRequest(targetUrl, COMMAND_OFF);
        //}


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
