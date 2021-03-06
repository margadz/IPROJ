﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.DataRepository;
using IPROJ.MSSQLRepository.Repository;
using IPROJ.MSSQLRepository.Tests;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Given_instance_of.DataRepository_class
{
    [TestFixture]
    public class when_providing_data
    {
        private static Guid _guid = Guid.Parse("D28B2B0C-831A-4027-9B6D-3894F5A7EB69");
        private IDataRepository _repository;
        private int _allReadingCount = 21;
        private int _allDevicesCount = 7;
        private int _readingsFromDeviceCount = 4;
        private IEnumerable<DeviceReading> _readings = new List<DeviceReading>()
        {
          new DeviceReading(DateTime.Now.AddMinutes(10), 46.4M, _guid, ReadingType.PowerConsumption, ReadingCharacter.Instant),
          new DeviceReading(DateTime.Now, 46.7M, _guid, ReadingType.PowerConsumption, ReadingCharacter.Instant)
        };

        private IEnumerable<DeviceReading> _incorrectReadings = new List<DeviceReading>()
        {
          new DeviceReading(DateTime.Now.AddMinutes(10), 46.4M, Guid.NewGuid(), ReadingType.PowerConsumption, ReadingCharacter.Instant),
          new DeviceReading(DateTime.Now, 46.7M, Guid.NewGuid(), ReadingType.PowerConsumption, ReadingCharacter.Instant)
        };

        [Test]
        public void DataRepository_GetAllReadingsAsync_Test()
        {
            _repository.GetAllReadingsAsync().Result.Count().Should().Be(_allReadingCount);
        }

        [Test]
        public void DataRepository_GetAllDevicesAsync_Tests()
        {
            _repository.GetAllDevicesAsync().Result.Count().Should().Be(_allDevicesCount);
        }

        [Test]
        public void DataRepository_GetAllReadingsFromDeviceAsync_Test()
        {
            _repository.GetAllReadingsFromDeviceAsync(_guid).Result.Count().Should().Be(_readingsFromDeviceCount);
        }

        [Test]
        public void DataRepository_AddReadingsAsync_Test()
        {
            _repository.AddReadingsAsync(_readings).Wait();

            _repository.GetAllReadingsAsync().Result.Count().Should().Be(_allReadingCount + _readings.Count());
        }

        [Test]
        public void DataRepository_UnknownDeviceId_Test()
        {
            _repository.Awaiting(instance => instance.AddReadingsAsync(_incorrectReadings))
                .Should().Throw<AggregateException>()
                .WithInnerException<DbUpdateException>();
        }

        [Test]
        public void DataRepository_Adding_device_Test()
        {
            var newDevice = new DeviceDescription() { Name = "NewDevice", TypeOfReading = ReadingType.PowerConsumption, TypeOfDevice = DeviceType.HS110, IsActive = true, Host = "someHost" };

            _repository.AddDeviceAync(newDevice).Wait();

            _repository.GetAllDevicesAsync().Result.Any(device => device.Name == newDevice.Name).Should().BeTrue();
        }

        [Test]
        public void DeviceRespository_modifying_device_Test()
        {
            var existingDevice = _repository.GetAllDevicesAsync().Result.First(device => device.Name == "Temperatura w pokoju");
            existingDevice.IsActive = false;

            _repository.AddDeviceAync(existingDevice).Wait();

            _repository.GetAllDevicesAsync().Result.Any(device => existingDevice.DeviceId == device.DeviceId && existingDevice.IsActive == device.IsActive).Should().BeTrue();
            _repository.GetAllDevicesAsync().Result.Any(device => existingDevice.DeviceId == device.DeviceId && existingDevice.IsActive != device.IsActive).Should().BeFalse();
        }

        [Test]
        public void DeviceRespository_should_not_modify_host_Test()
        {
            var existingDevice = _repository.GetAllDevicesAsync().Result.First(device => device.Name == "Temperatura w pokoju");
            existingDevice.Host = "NewHost";

            _repository.AddDeviceAync(existingDevice).Wait();

            _repository.GetAllDevicesAsync().Result.Any(device => existingDevice.DeviceId == device.DeviceId && existingDevice.Host== device.Host).Should().BeFalse();
            _repository.GetAllDevicesAsync().Result.Any(device => existingDevice.DeviceId == device.DeviceId && existingDevice.Host != device.Host).Should().BeTrue();
        }

        [SetUp]
        public void TestSetup()
        {
            DbSetup.SetupDb();
            _repository = new DataRepository(DbSetup.ConnectionString);
        }

        [OneTimeTearDown]
        public void FixtureTearDown()
        {
            DbSetup.SetupDb();
            _repository = new DataRepository(DbSetup.ConnectionString);
        }
    }
}
