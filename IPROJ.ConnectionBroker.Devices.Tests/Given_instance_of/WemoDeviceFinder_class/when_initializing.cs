using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Devices.Wemo.Discovery;
using NUnit.Framework;

namespace Given_instance_of.WemoDeviceFinder_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_configuration_provider_is_null()
        {
            ((WemoDeviceFinder)null).Invoking(_ => new WemoDeviceFinder(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
