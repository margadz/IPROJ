using System;
using FluentAssertions;
using IPROJ.Contracts.Logging;
using IPROJ.SignalR.SignalR;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.HS110Device_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_logger_is_null()
        {
            ((SignalRMessenger)null).Invoking(_ => new SignalRMessenger(null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_configuration_provider_is_null()
        {
            ((SignalRMessenger)null).Invoking(_ => new SignalRMessenger(new Mock<IInstantMessengerLogger>().Object, null)).Should().Throw<ArgumentNullException>();
        }
    }
}
