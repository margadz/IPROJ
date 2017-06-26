using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using Newtonsoft.Json;

namespace CB.WebApi
{
    public class WebRepository : IDevicesRepository
    {
        private Uri uri = new Uri("http://192.168.1.10:12345/api/devices/");
        private List<Device> _devices;

        public IList<Device> Devices
        {
            get
            {
                if (_devices == null)
                {
                    _devices = (List<Device>)GetAllActiveDevicesAsync().Result;
                }
                return _devices;
            }
        }

        public async Task<IList<Device>> GetAllActiveDevicesAsync()
        {
            List<Device> devices;
            using (var client = GetHttpClient())
            {
                HttpResponseMessage response = client.GetAsync("active").Result;
                var result = await response.Content.ReadAsStringAsync();

                devices = JsonConvert.DeserializeObject<List<Device>>(result);
            }
            return await Task.FromResult(devices);
        }

        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = uri;

            return client;
        }
    }
}
