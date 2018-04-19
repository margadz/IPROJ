using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.DevicesManager.Wemo;
using NUnit.Framework;

namespace IPROJ.Given_instance_of.WemoDevice_class
{
    [TestFixture]
    public class when_initializing
    {
        public void Should_throw_when_data_repository_is_null()
        {
            ((WemoDevice)null).Invoking(_ => new WemoDevice(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
