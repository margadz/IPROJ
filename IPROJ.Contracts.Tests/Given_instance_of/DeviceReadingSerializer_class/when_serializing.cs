﻿using System;
using System.Globalization;
using FluentAssertions;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Messaging;
using NUnit.Framework;

namespace Given_instance_of.DeviceReadingSerializer_class
{
    [TestFixture]
    public class when_serializing
    {
        private DeviceReading _reading = new DeviceReading()
        {
            Device = new DeviceDescription() { DeviceId = Guid.NewGuid() },
            DeviceId = Guid.NewGuid(),
            ReadingCharacter = ReadingCharacter.Daily,
            ReadingTimeStamp = DateTime.ParseExact(DateTime.UtcNow.ToString("yyMMddhhmmss"), "yyMMddhhmmss", CultureInfo.InvariantCulture),
            TypeOfReading = ReadingType.PowerConsumption,
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
