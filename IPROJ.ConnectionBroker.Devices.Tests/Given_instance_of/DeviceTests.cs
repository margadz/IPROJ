using System;
using IPROJ.Contracts;
using IPROJ.Contracts.Logging;
using Moq;
using NUnit.Framework;

namespace IPROJ
{
    [TestFixture]
    public class DeviceTests
    {
        protected Mock<IDeviceLog> Logger { get; private set; }

        [SetUp]
        public void TestSetup()
        {
            Logger = new Mock<IDeviceLog>(MockBehavior.Strict);
            Logger.Setup(_ => _.InformDeviceHasConnected(It.IsAny<IDevice>()));
            Logger.Setup(_ => _.RaiseErrorOnDeviceConnections(It.IsAny<Exception>(), It.IsAny<IDevice>()));
            Logger.Setup(_ => _.RaiseErrorOnGettingData(It.IsAny<Exception>(), It.IsAny<IDevice>()));
            Logger.Setup(_ => _.InformDeviceConnectionWasReestablished(It.IsAny<IDevice>()));
            ScenarioSetup();
        }

        protected virtual void ScenarioSetup()
        {
        }
    }
}
