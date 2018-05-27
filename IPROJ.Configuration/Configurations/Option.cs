using System;
using System.Collections.Generic;
using System.Text;

namespace IPROJ.Configuration.Configurations
{
    /// <summary>Describes configuration entry.</summary>
    public class Option
    {

        public Option(string category, string optionName, string optionValue)
        {
            Category = category;
            OptionName = optionName;
            OptionValue = optionValue;
        }

        /// <summary>Gets category of option.</summary>
        public string Category { get; }

        /// <summary>Gets name of option.</summary>
        public string OptionName { get; }

        /// <summary>Gets value of option.</summary>
        public string OptionValue { get; }
    }
}
