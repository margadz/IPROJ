using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Devices.Managing;
using Moq;
using NUnit.Framework;

namespace IPROJ.Given_instance_of.CompoundDeviceManager_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_data_repository_is_null()
        {
            ((CompoundDeviceManager)null).Invoking(_ => new CompoundDeviceManager(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_any_manager_is_null()
        {
            ((CompoundDeviceManager)null).Invoking(_ => new CompoundDeviceManager(new IDeviceManager[] {null, new Mock<IDeviceManager>().Object })).Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
