using System;
using FluentAssertions;
using IPROJ.SignalR.SignalR;
using NUnit.Framework;

namespace Given_instance_of.HS110Device_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_logger_is_null()
        {
            ((SignalRMessenger)null).Invoking(_ => new SignalRMessenger(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
