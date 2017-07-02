﻿using System;
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
        private static readonly Uri _uri = new Uri("http://192.168.1.10:12345/api/devices/");

        public async Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            using (var client = GetHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(string.Empty);
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
            client.BaseAddress = _uri;

            return client;
        }
    }
}
