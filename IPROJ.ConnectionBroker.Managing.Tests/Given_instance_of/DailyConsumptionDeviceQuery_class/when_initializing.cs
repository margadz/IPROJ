using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.DevicesManager;
using IPROJ.ConnectionBroker.Managing.Quering;
using IPROJ.Contracts;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.DailyConsumptionDeviceQuery_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_data_repository_is_null()
        {
            ((DailyConsumptionDeviceQuery)null).Invoking(_ => new DailyConsumptionDeviceQuery(null, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_device_repository_is_null()
        {
            ((DailyConsumptionDeviceQuery)null).Invoking(_ => new DailyConsumptionDeviceQuery(new Mock<IQueueWriter>().Object, null, null))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_configuration_provider_is_null()
        {
            ((DailyConsumptionDeviceQuery)null).Invoking(_ => new DailyConsumptionDeviceQuery(new Mock<IQueueWriter>().Object, new Mock<IDeviceRepository>().Object, null))
                .Should().Throw<ArgumentNullException>();
        }
    }
}
