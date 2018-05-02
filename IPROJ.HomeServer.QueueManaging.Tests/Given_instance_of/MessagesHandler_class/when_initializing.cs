using System;
using FluentAssertions;
using IPROJ.Contracts.DataRepository;
using IPROJ.Contracts.Messaging;
using IPROJ.HomeServer.QueueClient;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.MessagesHandler_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_data_repository_is_null()
        {
            ((MessagesHandler)null).Invoking(_ => new MessagesHandler(null, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_queue_listener_is_null()
        {
            ((MessagesHandler)null).Invoking(_ => new MessagesHandler(new Mock<IDataRepository>().Object, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_instant_messenger_is_null()
        {
            ((MessagesHandler)null).Invoking(_ => new MessagesHandler(new Mock<IDataRepository>().Object, new Mock<IQueueListener>().Object, null)).Should().Throw<ArgumentNullException>();
        }
    }
}
