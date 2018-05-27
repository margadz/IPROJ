using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Devices.HS110.Discovery;
using NUnit.Framework;

namespace Given_instance_of.HS110DeviceFinder_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_configuration_provider_is_null()
        {
            ((HS110DeviceFinder)null).Invoking(_ => new HS110DeviceFinder(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
