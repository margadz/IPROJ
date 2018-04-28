using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IPROJ.ConnectionBroker.Devices.Managing;
using IPROJ.Contracts.DataModel;
using Moq;
using NUnit.Framework;

namespace IPROJ.Given_instance_of.InstantMessurmentsDeviceManager_class
{
    [TestFixture]
    public class when_managing : DeviceManagerTests<InstantMessurmentsDeviceManager>
    {
        protected override InstantMessurmentsDeviceManager Manager
        {
            get { return new InstantMessurmentsDeviceManager(QueueWriter.Object, DeviceRepository.Object, ConfigurationProvider.Object); }
        }

        [Test]
        public void First_device_should_be_called()
        {
            TheTest();
            FirstDevice.Verify(_ => _.GetInsantReading(), Times.Once);
        }

        [Test]
        public void Second_device_should_be_called()
        {
            TheTest();
            SecondDevice.Verify(_ => _.GetInsantReading(), Times.Once);
        }

        [Test]
        public void Queue_writer_should_be_called()
        {
            TheTest();
            QueueWriter.Verify(_ => _.Put(It.IsAny<IEnumerable<DeviceReading>>(), It.IsAny<CancellationToken>()), Times.Once);
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
