using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Devices;
using NUnit.Framework;

namespace Given_instance_of.DeviceFactory_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_logger_is_null()
        {
            ((DeviceFactory)null).Invoking(_ => new DeviceFactory(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
