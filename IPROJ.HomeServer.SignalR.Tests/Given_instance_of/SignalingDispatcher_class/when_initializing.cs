using System;
using FluentAssertions;
using IPROJ.Contracts.Messaging;
using IPROJ.HomeServer.SignalR;
using Moq;
using NUnit.Framework;

namespace IPROJ.Given_instance_of.HS110Device_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_queue_listener_is_null()
        {
            ((SignalingDispatcher)null).Invoking(_ => new SignalingDispatcher(null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_logger_is_null()
        {
            ((SignalingDispatcher)null).Invoking(_ => new SignalingDispatcher(new Mock<IQueueListener>().Object, null)).Should().Throw<ArgumentNullException>();
        }
    }
}
