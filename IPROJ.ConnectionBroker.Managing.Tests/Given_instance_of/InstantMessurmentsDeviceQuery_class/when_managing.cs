﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IPROJ;
using IPROJ.ConnectionBroker.Managing.Quering;
using IPROJ.Contracts.DataModel;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.InstantMessurmentsDeviceQuery_class
{
    [TestFixture]
    public class when_managing : DeviceManagerTests<InstantMessurmentsDeviceQuery>
    {
        protected override InstantMessurmentsDeviceQuery Manager
        {
            get { return new InstantMessurmentsDeviceQuery(Messenger.Object, DeviceRepository.Object, ConfigurationProvider.Object); }
        }

        [Test]
        public void First_device_should_be_called()
        {
            TheTest();
            FirstDevice.Verify(_ => _.GetInsantReading(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Second_device_should_be_called()
        {
            TheTest();
            SecondDevice.Verify(_ => _.GetInsantReading(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Messenger_should_be_called()
        {
            TheTest();
            Messenger.Verify(_ => _.SendReadings(It.IsAny<IEnumerable<DeviceReading>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Actual_readings_should_be_sent()
        {
            TheTest();
            SentReadings.Any(_ => ReferenceEquals(_, FirstReading));
            SentReadings.Any(_ => ReferenceEquals(_, SecondReading));
        }
    }
}
