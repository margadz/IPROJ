using System;
using System.Collections.Generic;
using System.Threading;
using IPROJ.Configuration.Configurations;
using IPROJ.ConnectionBroker.Devices.Managing;
using IPROJ.Contracts.DataModel;
using Moq;
using NUnit.Framework;

namespace IPROJ.Given_instance_of.DailyConsumptionDeviceManager_class
{
    [TestFixture]
    public class when_managing : DeviceManagerTests<DailyConsumptionDeviceManager>
    {
        protected override DailyConsumptionDeviceManager Manager
        {
            get { return new DailyConsumptionDeviceManager(QueueWriter.Object, DeviceRepository.Object, ConfigurationProvider.Object); }
        }

        [Test]
        public void Queue_writer_should_be_called_after_set_time()
        {
            SetupTime(true);
            TheTest();
            QueueWriter.Verify(_ => _.Put(It.IsAny<IEnumerable<DeviceReading>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Queue_writer_should_not_be_called_before_set_time()
        {
            SetupTime(false);
            TheTest();
            QueueWriter.Verify(_ => _.Put(It.IsAny<IEnumerable<DeviceReading>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        private void SetupTime(bool passed)
        {
            int hours = 0;
            int minutes = 0;
            var time = DateTime.Now;
            if (passed)
            {
                hours = time.Hour - 1;
            }
            else
            {
                hours = time.Hour + 1;
            }

            ConfigurationProvider.Setup(_ => _.GetOption(ConnectionBrokerConfigurations.Category, ConnectionBrokerConfigurations.DailyConsumptionGettingTime)).Returns($"{hours}:{minutes}");
        }
    }
}
