using System.Linq;
using IPROJ.Configuration.Configurations;

namespace IPROJ.Configuration.ConfigurationProvider
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string GetOption(string optionCategory, string optionName)
        {
            return (from options in DefaultConfigurations.Options
                    where options.Category == optionCategory &&
                         options.OptionName == optionName
                    select options.OptionValue).FirstOrDefault();
        }
    }
}