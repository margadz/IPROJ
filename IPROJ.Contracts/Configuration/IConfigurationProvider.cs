using System;

namespace IPROJ.Contracts.ConfigurationProvider
{
    public interface IConfigurationProvider
    {
        string GetOption(string optionCategory, string optionName);
    }
}
