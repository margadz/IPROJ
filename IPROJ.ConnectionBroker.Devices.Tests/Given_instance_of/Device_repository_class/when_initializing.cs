using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.DevicesManager;
using NUnit.Framework;

namespace IPROJ.Given_instance_of.Device_repository_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_data_repository_is_null()
        {
            ((DeviceRepository)null).Invoking(_ => new DeviceRepository(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
