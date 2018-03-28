using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IPROJ.Contracts.DataModel;
using IPROJ.HomeServer.MSSQLRepository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace IPROJ.HomeServer.MSSQLRespository.Tests
{
    [TestFixture]
    public class DataRepositoryTests
    {
        private static Guid _guid = Guid.Parse("D28B2B0C-831A-4027-9B6D-3894F5A7EB69");
        private DataRepository _repository;
        private int _allReadingCount = 16;
        private int _allDevicesCount = 7;
        private int _readingsFromDeviceCount = 4;
        private IEnumerable<DeviceReading> _readings = new List<DeviceReading>()
        {
          new DeviceReading(DateTime.Now.AddMinutes(10), 46.4M, _guid, ReadingType.PowerConsumption.ToString()),
          new DeviceReading(DateTime.Now, 46.7M, _guid, ReadingType.PowerConsumption.ToString())
        };

        private IEnumerable<DeviceReading> _incorrectReadings = new List<DeviceReading>()
        {
          new DeviceReading(DateTime.Now.AddMinutes(10), 46.4M, Guid.NewGuid(), ReadingType.PowerConsumption.ToString()),
          new DeviceReading(DateTime.Now, 46.7M, Guid.NewGuid(), ReadingType.PowerConsumption.ToString())
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
