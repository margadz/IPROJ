using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Devices;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.Device_repository_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_data_repository_is_null()
        {
            ((DeviceRepository)null).Invoking(_ => new DeviceRepository(null, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_factory_is_null()
        {
            ((DeviceRepository)null).Invoking(_ => new DeviceRepository(new Mock<IDataRepository>().Object, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_threading_is_null()
        {
            ((DeviceRepository)null).Invoking(_ => new DeviceRepository(new Mock<IDataRepository>().Object, new Mock<IDeviceFactory>().Object, null)).Should().Throw<ArgumentNullException>();
        }
    }
}
