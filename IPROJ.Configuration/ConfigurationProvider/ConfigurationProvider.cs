using System.Linq;
using IPROJ.Configuration.Configurations;

namespace IPROJ.Configuration.ConfigurationProvider
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string GetOption(string OptionName, string OptionCategory)
        {
            return (from options in DefaultConfigurations.Options
                    where options.Category == OptionCategory &&
                         options.OptionName == OptionName
                    select options.OptionValue).FirstOrDefault();
        }
    }
}