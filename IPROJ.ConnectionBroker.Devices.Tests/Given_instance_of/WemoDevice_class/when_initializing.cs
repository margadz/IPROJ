using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Devices.Wemo.HttpCommunication;
using IPROJ.ConnectionBroker.DevicesManager.Wemo;
using IPROJ.Contracts.DataModel;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.WemoDevice_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_description_is_null()
        {
            ((WemoDevice)null).Invoking(_ => new WemoDevice(null, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_logger_is_null()
        {
            ((WemoDevice)null).Invoking(_ => new WemoDevice(new DeviceDescription(), new Mock<ISoapCaller>().Object, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_caller_is_null()
        {
            ((WemoDevice)null).Invoking(_ => new WemoDevice(new DeviceDescription(), null, null)).Should().Throw<ArgumentNullException>();
        }
    }
}
