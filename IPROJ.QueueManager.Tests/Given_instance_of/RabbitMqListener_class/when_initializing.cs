﻿using System;
using FluentAssertions;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.QueueManager.Connection;
using IPROJ.QueueManager.RabbitMQ;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.RabbitMqListener_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_connection_factory_is_null()
        {
            ((RabbitMqListener)null).Invoking(_ => new RabbitMqListener(null, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_configuration_provider_is_null()
        {
            ((RabbitMqListener)null).Invoking(_ => new RabbitMqListener(new Mock<IRabbitMqConnectionFactory>().Object, null, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Should_throw_when_logger_is_null()
        {
            ((RabbitMqListener)null).Invoking(_ => new RabbitMqListener(new Mock<IRabbitMqConnectionFactory>().Object, new Mock<IConfigurationProvider>().Object, null)).Should().Throw<ArgumentNullException>();
        }
    }
}
