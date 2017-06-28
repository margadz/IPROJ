using System.Collections.Generic;

namespace IPROJ.Configuration.Configurations
{
    internal static class DefaultConfigurations
    {
        internal static readonly IEnumerable<Option> Options = new List<Option>()
        {
            new Option("Core", "MQServerIp", "192.168.1.10"),
            new Option("Core", "MQServerPort", "5672"),
            new Option("Core", "MQServerVHost", @"/"),
            new Option("Core", "CodePage", "65001"),
            new Option("Core", "ReadingsQueue", "HomeServerReadings"),
            new Option("Core", "ReadingsExchange", "ReadingsExchange"),
            new Option("Core", "RoutingKey", "readings"),
            new Option("ConnectionBroker", "MQServerUser", "CBUser"),
            new Option("ConnectionBroker", "MQServerPass", "CBPass"),
            new Option("HomeServer", "MQServerUser", "HSUser"),
            new Option("HomeServer", "MQServerPass", "HSPass")
        };
    }
}