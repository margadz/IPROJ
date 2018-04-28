using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.DevicesManager.HS110;
using IPROJ.Contracts.DataModel;
using NUnit.Framework;

namespace IPROJ.Given_instance_of.HS110Device_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_description_is_null()
        {
            ((HS110Device)null).Invoking(_ => new HS110Device(null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_logger_is_null()
        {
            ((HS110Device)null).Invoking(_ => new HS110Device(new DeviceDescription(), null)).Should().Throw<ArgumentNullException>();
        }
    }
}
