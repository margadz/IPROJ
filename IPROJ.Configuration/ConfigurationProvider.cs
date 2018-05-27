using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Configuration.Configurations;
using IPROJ.Contracts.ConfigurationProvider;
using Newtonsoft.Json;

namespace IPROJ.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private IEnumerable<Option> _options;
        private readonly ManualResetEvent _syncEvent = new ManualResetEvent(false);

        /// <summary>Initializes instance of <see cref="ConfigurationProvider"/>.</summary>
        public ConfigurationProvider()
        {
            Task.Run((Action)InitializeConfiguration);
        }

        public string GetOption(string optionCategory, string optionName)
        {
            _syncEvent.WaitOne();
            return (from options in _options
                    where options.Category == optionCategory &&
                         options.OptionName == optionName
                    select options.OptionValue).FirstOrDefault();
        }

        private async void InitializeConfiguration()
        {
            using (var reader = new StreamReader("config.json"))
            {
                var json = await reader.ReadToEndAsync();
                _options = JsonConvert.DeserializeObject<IEnumerable<Option>>(json);
            }

            _syncEvent.Set();
        }
    }
}