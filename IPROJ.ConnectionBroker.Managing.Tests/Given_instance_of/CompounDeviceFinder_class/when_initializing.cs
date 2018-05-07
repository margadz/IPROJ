using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Managing.Discovery;
using IPROJ.Contracts.Device.Discovery;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.CompounDeviceFinder_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_finders_collection_is_null()
        {
            ((CompoundDeviceFinder)null).Invoking(_ => new CompoundDeviceFinder(null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_logger_is_null()
        {
            ((CompoundDeviceFinder)null).Invoking(_ => new CompoundDeviceFinder(new[] { new Mock<IDeviceFinder>().Object }, null))
                .Should().Throw<ArgumentNullException>();
        }

    }
}
