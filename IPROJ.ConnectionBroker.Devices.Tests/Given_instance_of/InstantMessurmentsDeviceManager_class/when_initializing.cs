using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Devices.Managing;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.Contracts;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.DailyConsumptionDeviceManager_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_data_repository_is_null()
        {
            ((DailyConsumptionDeviceManager)null).Invoking(_ => new DailyConsumptionDeviceManager(null, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_device_repository_is_null()
        {
            ((DailyConsumptionDeviceManager)null).Invoking(_ => new DailyConsumptionDeviceManager(new Mock<IQueueWriter>().Object, null, null))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_configuration_provider_is_null()
        {
            ((DailyConsumptionDeviceManager)null).Invoking(_ => new DailyConsumptionDeviceManager(new Mock<IQueueWriter>().Object, new Mock<IDeviceRepository>().Object, null))
                .Should().Throw<ArgumentNullException>();
        }
    }
}
