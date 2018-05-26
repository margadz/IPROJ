using System;
using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using IPROJ.ConnectionBroker.Managing.Discovery;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Device.Discovery;
using IPROJ.Contracts.Logging;
using Moq;
using NUnit.Framework;

namespace Given_instance_of.CompounDeviceFinder_class
{
    [TestFixture]
    public class when_finding
    {
        private Mock<IDeviceFinder> _firstFinder;
        private Mock<IDeviceFinder> _secondFinder;
        private Mock<IDeviceFinderLogger> _logger;
        private CompoundDeviceFinder _finder;
        private IEnumerable<DeviceDescription> _result;

        public void TheTest(bool withError = false)
        {
            if (withError)
            {
                _firstFinder.Setup(_ => _.Discover(It.IsAny<CancellationToken>())).Throws<Exception>();
            }

            _result = _finder.Discover(CancellationToken.None).Result;
        }

        [Test]
        public void Should_call_first_inner_finder()
        {
            TheTest();
            _firstFinder.Verify(_ => _.Discover(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Should_call_second_inner_finder()
        {
            TheTest();
            _secondFinder.Verify(_ => _.Discover(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Should_return_correct_number_of_devices()
        {
            TheTest();
            _result.Should().HaveCount(2);
        }

        [Test]
        public void Should_log_on_succesfull_discovery()
        {
            TheTest();
            _logger.Verify(_ => _.InformWhenDeviceDiscoveryHasFinished(2), Times.Once);
        }

        [Test]
        public void Should_log_when_inner_finder_throws_exception()
        {
            TheTest(true);
            _logger.Verify(_ => _.RaiseOnErrorDuringDiscover(It.IsAny<Exception>(), It.IsAny<IDeviceFinder>()), Times.Once);
        }

        [Test]
        public void Should_return_correct_number_of_devices_after_error()
        {
            TheTest(true);
            _result.Should().HaveCount(1);
        }

        [SetUp]
        public void ScenarioSetup()
        {
            _firstFinder = new Mock<IDeviceFinder>(MockBehavior.Strict);
            _firstFinder.Setup(_ => _.Discover(It.IsAny<CancellationToken>())).ReturnsAsync(new[] { new DeviceDescription() });
            _secondFinder = new Mock<IDeviceFinder>(MockBehavior.Strict);
            _secondFinder.Setup(_ => _.Discover(It.IsAny<CancellationToken>())).ReturnsAsync(new[] { new DeviceDescription() });
            _logger = new Mock<IDeviceFinderLogger>(MockBehavior.Strict);
            _logger.Setup(_ => _.InformWhenDeviceDiscoveryHasFinished(It.IsAny<int>()));
            _logger.Setup(_ => _.RaiseOnErrorDuringDiscover(It.IsAny<Exception>(), It.IsAny<IDeviceFinder>()));
            _finder = new CompoundDeviceFinder(new []{ _firstFinder.Object, _secondFinder.Object }, _logger.Object);
        }
    }
}
