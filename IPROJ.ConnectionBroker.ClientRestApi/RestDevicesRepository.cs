using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IPROJ.ConnectionBroker.ClientRestApi
{
    public class RestDevicesRepository : IDevicesRepository
    {
        private static readonly Uri Uri = new Uri("http://192.168.1.10:12345/api/devices/");

        public Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            using (var client = GetHttpClient())
            {
                /*HttpResponseMessage response = null;
                try
                {
                    response = await client.GetAsync(string.Empty);
                }
                catch (HttpRequestException)
                {
                    return new List<Device>();
                }

                var result = await response.Content.ReadAsStringAsync();
                */
                var device = new Device()
                {
                    CustomId = "8006D1847073EC74595FFCD43771CB2817AFBCAD",
                    DeviceId = Guid.Parse("3E94D714-9652-446D-BB66-2EA84F5E49BD"),
                    Host = "192.168.1.202:9999",
                    IsActive = true,
                    TypeOfReading = "PowerConsumption",
                    Name = "Komputer w salonie"
                };

                return Task.FromResult<IEnumerable<Device>>(new Device[] { device });

                //return JsonConvert.DeserializeObject<IEnumerable<Device>>(result);
            }
        }

        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = Uri;

            return client;
        }
    }
}
