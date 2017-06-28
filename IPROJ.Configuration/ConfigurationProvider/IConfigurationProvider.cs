using System.Collections.Generic;

namespace IPROJ.Configuration.ConfigurationProvider
{
    public interface IConfigurationProvider
    {
        string GetOption(string OptionName, string OptionCategory);
    }
}
