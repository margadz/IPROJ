using System;

namespace IPROJ.Contracts.ConfigurationProvider
{
    /// <summary>Providers configuration.</summary>
    public interface IConfigurationProvider
    {
        /// <summary>Gets configuration entry.</summary>
        /// <param name="optionCategory">Option category.</param>
        /// <param name="optionName">Option name.</param>
        /// <returns>Configuration entry.</returns>
        string GetOption(string optionCategory, string optionName);
    }
}
