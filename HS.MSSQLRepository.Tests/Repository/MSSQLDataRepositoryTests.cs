using HS.MSSQLRepository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace HS.MSSQLRepository.Tests.Repository
{
    [TestFixture]
    public class MSSQLDataRepositoryTests
    {
        private MSSQLDataRepository repository = new MSSQLDataRepository(@"Data Source=KOMP;Initial Catalog=HomeServerTests;Integrated Security=True");
        private int allReadings = 25;
        private int allDevices = 10;
        private int allActiveDevices = 9;
        private int temperatureReadings = 9;
        private Guid activeDeviceGuid = new Guid("8317030D-8C3E-4061-B56B-A263060CBE4D");
        private int readingFromGuid = 3;
        private decimal[] lastReadings = new decimal[] { 21.10M, 22.20M, 120.20M, 22.10M, 50.10M, 41.20M, 40.10M };


        [Test]
        public void MSSQLDataRepositoryGetAllReadingTest()
        {
            var task = repository.GetAllReadingsAsync();

            Assert.AreEqual(allReadings, task.Result.Count);
        }

        [Test]
        public void MSSQLDataRepositoryGetAllActiveDevicesAsyncTest()
        {
            var task = repository.GetAllActiveDevicesAsync();

            var result = task.Result;

            Assert.AreEqual(allActiveDevices, result.Count);
            foreach (var device in result)
            {
                Assert.IsTrue(device.IsActive);
            }
        }

        [Test]
        public void MSSQLDataRepositoryGetAllDevicesAsyncTest()
        {
            var task = repository.GetAllDevicesAsync();

            Assert.AreEqual(allDevices, task.Result.Count);
        }

        [Test]
        public void MSSQLDataRepositoryGetAllInactiveDevicesAsync()
        {
            var task = repository.GetAllInactiveDevicesAsync();

            var result = task.Result;

            Assert.AreEqual(allActiveDevices, result.Count);
            foreach (var device in result)
            {
                Assert.IsFalse(device.IsActive);
            }
        }
        
        [Test]
        public void MSSQLDataRepositoryGetAllReadingsFromDeviceAsync()
        {

            var task = repository.GetAllReadingsFromDeviceAsync(activeDeviceGuid);

            Assert.AreEqual(task.Result.Count, readingFromGuid);
            Assert.IsNotNull(task.Result.Where(x => x.Value == 23.80M));
        }

        [Test]
        public void MSSQLDataRepositoryGetAllReadingsOfTypeAsyncTest()
        {
            var task = repository.GetAllReadingsOfTypeAsync("Temperature");
            Assert.AreEqual(task.Result.Count, temperatureReadings);
            Assert.IsNotNull(task.Result.Where(x => x.Value == 24.20M).FirstOrDefault());
            Assert.IsNotNull(task.Result.Where(x => x.Value == 23.80M).FirstOrDefault());
        }

        [Test]
        public void MSSQLDataRepositoryGetAllReadingsSinceAsyncTest()
        {
            Assert.Fail();
        }

        [Test]
        public void MSSQLDataRepositoryGetLastReadingsAsyncTest()
        {
            var task = repository.GetLastReadingsAsync();

            var readings = (from read in task.Result
                            select read.Value).ToList();

            readings.Should().BeEquivalentTo(lastReadings);
        }

        [Test]
        public void MSSQLDataRepositoryAddDeviceAsyncTest()
        {
            Assert.Fail();
        }

        [Test]
        public void MSSQLDataRepositoryAddReadingAsyncTest()
        {
            Assert.Fail();
        }

        [Test]
        public void MSSQLDataRepositoryRemoveDeviceAsyncTest()
        {
            Assert.Fail();
        }

        [Test]
        public void MSSQLDataRepositorySetDeviceActivityAsyncTest()
        {
            Assert.Fail();
        }
    }
}
