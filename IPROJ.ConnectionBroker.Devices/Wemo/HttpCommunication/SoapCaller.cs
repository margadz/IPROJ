using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.Devices.Wemo.Commands;
using IPROJ.Contracts.Helpers;

namespace IPROJ.ConnectionBroker.Devices.Wemo.HttpCommunication
{
    /// <summary>Default implementation of <see cref="ISoapCaller"/>.</summary>
    public class SoapCaller : ISoapCaller
    {
        private readonly string _url;

        /// <summary>Initilizes instance of <see cref="SoapCaller"/>.</summary>
        /// <param name="baseUrl">Base url.</param>
        public SoapCaller(string baseUrl)
        {
            Argument.OfWichValueShoulBeProvided(baseUrl, nameof(baseUrl));

            _url = baseUrl;
        }

        public async Task<string> SendRequest(IWemoCommand command)
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
