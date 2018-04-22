using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Devices.Managing;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.Contracts;
using Moq;
using NUnit.Framework;

namespace IPROJ.Given_instance_of.InstantMessurmentsDeviceManager_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_data_repository_is_null()
        {
            ((InstantMessurmentsDeviceManager)null).Invoking(_ => new InstantMessurmentsDeviceManager(null, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_device_repository_is_null()
        {
            ((InstantMessurmentsDeviceManager)null).Invoking(_ => new InstantMessurmentsDeviceManager(new Mock<IQueueWriter>().Object, null, null))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_configuration_provider_is_null()
        {
            ((InstantMessurmentsDeviceManager)null).Invoking(_ => new InstantMessurmentsDeviceManager(new Mock<IQueueWriter>().Object, new Mock<IDeviceRepository>().Object, null))
                .Should().Throw<ArgumentNullException>();
        }
    }
}
