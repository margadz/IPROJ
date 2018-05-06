using System;
using FluentAssertions;
using IPROJ.ConnectionBroker.Managing.Quering;
using NUnit.Framework;

namespace Given_instance_of.CompoundDeviceQuery_class
{
    [TestFixture]
    public class when_initializing
    {
        [Test]
        public void Should_throw_when_data_repository_is_null()
        {
            ((CompoundDeviceQuery)null).Invoking(_ => new CompoundDeviceQuery(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
