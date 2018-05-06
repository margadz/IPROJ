using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Managing.Quering;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.CompoundDeviceQuery_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_data_repository_is_null()
        {
            ((CompoundDeviceQuery)null).Invoking(_ => new CompoundDeviceQuery(null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_messenger_is_null()
        {
            ((CompoundDeviceQuery)null).Invoking(_ => new CompoundDeviceQuery(Array.Empty<IDeviceQuery>(), null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_any_manager_is_null()
        {
            ((CompoundDeviceQuery)null).Invoking(_ => new CompoundDeviceQuery(new IDeviceQuery[] {null, new Mock<IDeviceQuery>().Object }, null)).Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
