using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using IPROJ.Configuration;
using IPROJ.Configuration.Configurations;
using NUnit.Framework;

namespace Given_instance_of.ConfigurationProvider_class
{
    [TestFixture]
    public class when_providing_configuration
    {
        private ConfigurationProvider _provider;

        [Test]
        [TestCaseSource(nameof(ConnectionBrokerTests))]
        [TestCaseSource(nameof(CoreTests))]
        [TestCaseSource(nameof(HomeServerTests))]
        public void Should_have_options_set_up(string category, string name)
        {
            _provider.GetOption(category, name).Should().NotBeNull();
        }

        [Test]
        public void Should_get_correct_options()
        {
            _provider.GetOption(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.InstanQueryInterval).Should().Be("00:00:01");
        }

        [SetUp]
        public void ScenarioSetup()
        {
            _provider = new ConfigurationProvider();
        }

        private static IEnumerable<TestCaseData> ConnectionBrokerTests()
        {
            var properties = typeof(ConnectionBrokerConfigurations).GetFields(BindingFlags.Public | BindingFlags.Static);
            var category = properties.First(_ => _.Name == "Category").GetValue(null);

            return from prop in properties
                   where prop.Name != "Category"
                   let value = (string)prop.GetValue(null)
                   select new TestCaseData(category, value);
        }

        private static IEnumerable<TestCaseData> CoreTests()
        {
            var properties = typeof(CoreConfigurations).GetFields(BindingFlags.Public | BindingFlags.Static);
            var category = properties.First(_ => _.Name == "Category").GetValue(null);

            return from prop in properties
                   where prop.Name != "Category"
                   let value = (string)prop.GetValue(null)
                   select new TestCaseData(category, value);
        }

        private static IEnumerable<TestCaseData> HomeServerTests()
        {
            var properties = typeof(HomeServerConfiguration).GetFields(BindingFlags.Public | BindingFlags.Static);
            var category = properties.First(_ => _.Name == "Category").GetValue(null);

            return from prop in properties
                   where prop.Name != "Category"
                   let value = (string)prop.GetValue(null)
                   select new TestCaseData(category, value);
        }
    }
}
