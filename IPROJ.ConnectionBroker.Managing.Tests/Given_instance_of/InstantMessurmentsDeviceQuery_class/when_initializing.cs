using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.ConnectionBroker.Managing.Quering;
using IPROJ.Contracts.Messaging;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.InstantMessurmentsDeviceQuery_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_data_repository_is_null()
        {
            ((InstantMessurmentsDeviceQuery)null).Invoking(_ => new InstantMessurmentsDeviceQuery(null, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_device_repository_is_null()
        {
            ((InstantMessurmentsDeviceQuery)null).Invoking(_ => new InstantMessurmentsDeviceQuery(new Mock<IMessenger>().Object, null, null))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_configuration_provider_is_null()
        {
            ((InstantMessurmentsDeviceQuery)null).Invoking(_ => new InstantMessurmentsDeviceQuery(new Mock<IMessenger>().Object, new Mock<IDeviceRepository>().Object, null))
                .Should().Throw<ArgumentNullException>();
        }
    }
}
