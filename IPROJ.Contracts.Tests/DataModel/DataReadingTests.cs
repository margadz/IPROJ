using System;
using System.Globalization;
using FluentAssertions;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Messaging;
using NUnit.Framework;

namespace IPROJ.Contracts.Tests.DataModel
{
    [TestFixture]
    public class DataReadingTests
    {
        private DeviceReading _reading = new DeviceReading()
        {
            Device = new DeviceDescription() { DeviceId = Guid.NewGuid() },
            DeviceId = Guid.NewGuid(),
            ReadingCharacter = ReadingCharacter.Daily,
            ReadingTimeStamp = DateTime.ParseExact(DateTime.UtcNow.ToString("yyMMddhhmmss"), "yyMMddhhmmss", CultureInfo.InvariantCulture),
            TypeOfReading = ReadingType.PowerComsumption,
            Value = 90m
        };

        [Test]
        public void Serialization_should_be_reversible()
        {
            var serialized = DeviceReadingSerializer.SerializeDeviceReading(_reading);
            DeviceReadingSerializer.DeserializeReading(serialized).Should().BeEquivalentTo(_reading);
        }
    }
}
