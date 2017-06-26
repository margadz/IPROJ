using System.Collections.Generic;

namespace IPROJ.Configuration.ConfigurationProvider
{
    public interface IConfigurationProvider
    {
        T GetOption<T>(string OptionName, string OptionCategory);

        IEnumerable<T> GetOptions<T>(string OptionName, string OptionCategory);
    }
}
