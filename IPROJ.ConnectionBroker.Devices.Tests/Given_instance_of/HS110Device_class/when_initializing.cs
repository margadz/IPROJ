using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Devices.HS110;
using IPROJ.ConnectionBroker.Devices.HS110.TcpCommunication;
using IPROJ.Contracts.DataModel;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.HS110Device_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_description_is_null()
        {
            ((HS110Device)null).Invoking(_ => new HS110Device(null, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_logger_is_null()
        {
            ((HS110Device)null).Invoking(_ => new HS110Device(new DeviceDescription(), new Mock<IHS110TcpConnector>().Object, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_connector_is_null()
        {
            ((HS110Device)null).Invoking(_ => new HS110Device(new DeviceDescription(), null, null)).Should().Throw<ArgumentNullException>();
        }
    }
}
