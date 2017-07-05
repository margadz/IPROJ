using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using Newtonsoft.Json;

namespace CB.ClientRestApi
{
    public class RestDevicesRepository : IDevicesRepository
    {
        private static readonly Uri Uri = new Uri("http://192.168.1.10:12345/api/devices/");

        public async Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            using (var client = GetHttpClient())
            {
                HttpResponseMessage response = null;
                try
                {
                    response = await client.GetAsync(string.Empty);
                }
                catch (HttpRequestException)
                {
                    return new List<Device>();
                }

                var result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<Device>>(result);
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
