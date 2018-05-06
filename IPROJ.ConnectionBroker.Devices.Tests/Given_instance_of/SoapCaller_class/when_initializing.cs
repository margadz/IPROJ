using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Devices.Wemo.HttpCommunication;
using NUnit.Framework;

namespace Given_instance_of.SoapCaller_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_urk_is_null()
        {
            ((SoapCaller)null).Invoking(_ => new SoapCaller(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
