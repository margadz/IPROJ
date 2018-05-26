using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts;
using IPROJ.Contracts.DataModel;

namespace IPROJ.Dummies.Devices
{
    public class DummyDevice : IDevice
    {
        private readonly Random _random = new Random();
        private DeviceState _state = DeviceState.On;
        private readonly int _mean;

        public DummyDevice(DeviceDescription deviceDescription)
        {
            DeviceId = deviceDescription.DeviceId;
            DeviceName = deviceDescription.Name;
            TypeOfReading = deviceDescription.TypeOfReading;
            _mean = _random.Next(60, 180);
            Console.WriteLine($"Dummy device of id {DeviceId} created.");
        }

        public Guid DeviceId { get; }

        public string DeviceName { get; }

        public ReadingType TypeOfReading { get; }

        public void Dispose()
        {
        }

        public Task<DeviceReading> GetInsantReading(CancellationToken cancellationToken)
        {
            var result = new DeviceReading()
            {
                DeviceId = DeviceId,
                DeviceState = _state,
                ReadingCharacter = ReadingCharacter.Instant,
                Value = _state == DeviceState.On ? (decimal)(_random.NextDouble() * 10 + _mean) : 0m
            };
            return Task.FromResult(result);
        }

        public Task<DeviceReading> GetTodaysConsumption(CancellationToken cancellationToken)
        {
            var result = new DeviceReading()
            {
                DeviceId = DeviceId,
                DeviceState = _state,
                ReadingCharacter = ReadingCharacter.Daily,
                Value = _state == DeviceState.On ? (decimal)(_random.NextDouble() * 10) : 0m
    };
            return Task.FromResult(result);
        }

        public Task SetState(DeviceState deviceState)
        {
            _state = deviceState;
            return Task.FromResult(0);
        }
    }
}
