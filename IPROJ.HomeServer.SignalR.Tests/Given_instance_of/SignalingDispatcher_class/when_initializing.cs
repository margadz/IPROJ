using System;
using FluentAssertions;
using IPROJ.HomeServer.SignalR;
using NUnit.Framework;

namespace Given_instance_of.HS110Device_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_logger_is_null()
        {
            ((SignalRInstantMessenger)null).Invoking(_ => new SignalRInstantMessenger(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
