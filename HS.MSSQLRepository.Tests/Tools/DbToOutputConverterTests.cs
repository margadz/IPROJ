using HS.Common.Enumerators;
using HS.Common.Exception;
using HS.MSSQLRepository.ModelData;
using HS.MSSQLRepository.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace HS.MSSQLRepository.Tests.Tools
{
    [TestClass]
    public class DbToOutputConverterTests
    {
        private Guid guid1 = Guid.NewGuid();
        private Guid guid2 = Guid.NewGuid();
        private InstrumentReadings reading1 = new InstrumentReadings();
        private InstrumentReadings reading2 = new InstrumentReadings();
        private Devices device1 = new Devices();
        private Devices device2 = new Devices();
        private Devices deviceWithInvalidType = new Devices();
        private List<InstrumentReadings> readingList;
        private List<Devices> completeDevicesList;
        private List<Devices> incompleteDevicesList;
        private List<Devices> invalidTypeDeviceList;
        private DateTime timeStamp1 = new DateTime(2016, 10, 10, 10, 10, 10);
        private DateTime timeStamp2 = new DateTime(2016, 11, 11, 11, 11, 11);
        private decimal value1 = 20.5M;
        private decimal value2 = 21.5M;
        private string deviceName1 = "device1";
        private string deviceName2 = "device2";
        private string type = ReadingType.Temperature.ToString();
        private string invalidType = "some type";

        [TestInitialize]
        public void TestSetup()
        {
            device1.Name = deviceName1;
            device1.IsActive = true;
            device1.ReadingInterval = 0;
            device1.TypeOfReading = type;
            device1.DeviceId = guid1;

            device2.Name = deviceName2;
            device2.IsActive = true;
            device2.ReadingInterval = null;
            device2.TypeOfReading = type;
            device2.DeviceId = guid2;

            deviceWithInvalidType.Name = deviceName1;
            deviceWithInvalidType.IsActive = true;
            deviceWithInvalidType.ReadingInterval = 0;
            deviceWithInvalidType.TypeOfReading = invalidType;
            deviceWithInvalidType.DeviceId = guid2;

            reading1.Device = device1;
            reading1.DeviceId = guid1;
            reading1.Value = value1;
            reading1.ReadingTimeStamp = timeStamp1;

            reading2.Device = device2;
            reading2.DeviceId = guid2;
            reading2.Value = value2;
            reading2.ReadingTimeStamp = timeStamp2;

            readingList = new List<InstrumentReadings>() { reading1, reading2 };

            completeDevicesList = new List<Devices>() { device1, device2 };
            incompleteDevicesList = new List<Devices> { device1 };
            invalidTypeDeviceList = new List<Devices> { device1, deviceWithInvalidType };

        }

        [Test]
        public void DbToOutputConverterSuccessfullReadingConvertTest()
        {
            var result = DbToOutputConverter.GetDeviceReadingFromDbEntities(readingList, completeDevicesList);

            Assert.AreEqual(result.Count, readingList.Count);
            Assert.AreEqual(result[0].DeviceId, reading1.DeviceId);
            Assert.AreEqual(result[1].DeviceId, reading2.DeviceId);
        }

        [Test]
        public void DbToOutputConverterDeviceGuidNotFoundTest()
        {
            Assert.ThrowsException<ConverterException>(delegate { DbToOutputConverter.GetDeviceReadingFromDbEntities(readingList, incompleteDevicesList); },
                                                                $"DeviceId: {guid2} could not been found in devices list");
        }

        [Test]
        public void DbToOutputConverterInvalidReadingTypeTest()
        {
            Assert.ThrowsException<ReadingTypeCastException>(delegate { DbToOutputConverter.GetDeviceReadingFromDbEntities(readingList, invalidTypeDeviceList); },
                                                                $"A type: {invalidType} is not valid reading type");

        }

        [Test]
        public void DbToOutputConverterDeviceConvertTest()
        {
            var result = DbToOutputConverter.GetDevicesFromDbEntities(completeDevicesList);

            Assert.AreEqual(result.Count, completeDevicesList.Count);
            Assert.AreEqual(result[0].DeviceId, reading1.DeviceId);
            Assert.AreEqual(result[1].DeviceId, reading2.DeviceId);
        }

        [Test]
        public void DbToOutputConverterDeviceConvertInvalidTypeTest()
        {
            Assert.ThrowsException<ReadingTypeCastException>(delegate { DbToOutputConverter.GetDevicesFromDbEntities(invalidTypeDeviceList); },
                                                    $"A type: {invalidType} is not valid reading type");
        }
    }
}
