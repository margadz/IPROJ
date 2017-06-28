using System;
using System.Collections.Generic;

namespace IPROJ.Configuration.ConfigurationProvider
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public T GetOption<T>(string OptionName, string OptionCategory)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetOptions<T>(string OptionName, string OptionCategory)
        {
            throw new NotImplementedException();
        }
    }
}
