using System;
using System.Collections.Generic;
using System.Text;

namespace IPROJ.Configuration.Configurations
{
    public class Option
    {
        public Option(string category, string optionName, string optionValue)
        {
            Category = category;
            OptionName = optionName;
            OptionValue = optionValue;
        }

        public string Category { get; }

        public string OptionName { get; }

        public string OptionValue { get; }
    }
}
