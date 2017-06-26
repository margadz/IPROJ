using HS.MSSQLRepository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using FluentAssertions;

namespace HS.MSSQLRepository.Tests.Repository
{
    [TestClass]
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


        [TestMethod]
        public void MSSQLDataRepositoryGetAllReadingTest()
        {
            var task = repository.GetAllReadingsAsync();

            Assert.AreEqual(allReadings, task.Result.Count);
        }

        [TestMethod]
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

        [TestMethod]
        public void MSSQLDataRepositoryGetAllDevicesAsyncTest()
        {
            var task = repository.GetAllDevicesAsync();

            Assert.AreEqual(allDevices, task.Result.Count);
        }

        [TestMethod]
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
        
        [TestMethod]
        public void MSSQLDataRepositoryGetAllReadingsFromDeviceAsync()
        {

            var task = repository.GetAllReadingsFromDeviceAsync(activeDeviceGuid);

            Assert.AreEqual(task.Result.Count, readingFromGuid);
            Assert.IsNotNull(task.Result.Where(x => x.Value == 23.80M));
        }

        [TestMethod]
        public void MSSQLDataRepositoryGetAllReadingsOfTypeAsyncTest()
        {
            var task = repository.GetAllReadingsOfTypeAsync("Temperature");
            Assert.AreEqual(task.Result.Count, temperatureReadings);
            Assert.IsNotNull(task.Result.Where(x => x.Value == 24.20M).FirstOrDefault());
            Assert.IsNotNull(task.Result.Where(x => x.Value == 23.80M).FirstOrDefault());
        }

        [TestMethod]
        public void MSSQLDataRepositoryGetAllReadingsSinceAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void MSSQLDataRepositoryGetLastReadingsAsyncTest()
        {
            var task = repository.GetLastReadingsAsync();

            var readings = (from read in task.Result
                            select read.Value).ToList();

            readings.Should().BeEquivalentTo(lastReadings);
        }

        [TestMethod]
        public void MSSQLDataRepositoryAddDeviceAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void MSSQLDataRepositoryAddReadingAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void MSSQLDataRepositoryRemoveDeviceAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void MSSQLDataRepositorySetDeviceActivityAsyncTest()
        {
            Assert.Fail();
        }
    }
}
