using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Managing;
using IPROJ.ConnectionBroker.Managing.Quering;
using IPROJ.Contracts.Device.Discovery;
using IPROJ.Contracts.Messaging;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.DeviceManager_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_query_is_null()
        {
            ((DeviceManager)null).Invoking(_ => new DeviceManager(null, null, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_manager_is_null()
        {
            ((DeviceManager)null).Invoking(_ => new DeviceManager(new Mock<IDeviceQuery>().Object, null, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_finder_is_null()
        {
            ((DeviceManager)null).Invoking(_ => new DeviceManager(new Mock<IDeviceQuery>().Object, new Mock<IMessenger>().Object, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_repository_is_null()
        {
            ((DeviceManager)null).Invoking(_ => new DeviceManager(new Mock<IDeviceQuery>().Object, new Mock<IMessenger>().Object, new Mock<IDeviceFinder>().Object, null)).Should().Throw<ArgumentNullException>();
        }
    }
}
