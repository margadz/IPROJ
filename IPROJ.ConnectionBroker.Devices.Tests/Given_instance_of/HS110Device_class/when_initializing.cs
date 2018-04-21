using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.DevicesManager.HS110;
using NUnit.Framework;

namespace IPROJ.Given_instance_of.HS110Device_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_data_repository_is_null()
        {
            ((HS110Device)null).Invoking(_ => new HS110Device(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
