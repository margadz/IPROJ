using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;

namespace IPROJ.ConnectionBroker.Devices
{
    /// <summary>Provides base functionality of <see cref="IDevice"/></summary>
    public abstract class Device : IDevice
    {
        private readonly ManualResetEvent _initSync = new ManualResetEvent(false);
        private CancellationToken _cancellationToken;
        private object _locker = new object();
        private bool _isDisposed;
        private bool _isConnected = true;
        private bool _reconnecting;

        public Device(IDeviceLog logger)
        {
            Argument.OfWichValueShoulBeProvided(logger, nameof(logger));
            Logger = logger;
        }

        protected IDeviceLog Logger { get; }

        /// <inheritdoc />
        public abstract Guid DeviceId { get; }

        /// <inheritdoc />
        public abstract string DeviceName { get; }

        /// <inheritdoc />
        public abstract ReadingType TypeOfReading { get; }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                _isDisposed = true;
            }
        }


        public async Task<DeviceReading> GetInsantReading(CancellationToken cancellationToken)
        {
            if (_cancellationToken == default(CancellationToken))
            {
                _cancellationToken = cancellationToken;
            }
            _initSync.WaitOne();
            if (!_isConnected)
            {
                return null;
            }

            try
            {
                return await InternalInstantGet();
            }
            catch (Exception error)
            {
                Logger.RaiseErrorOnGettingData(error, this);
                _isConnected = false;
                Task.Factory.StartNew(ReConnect, cancellationToken);
            }

            return null;
        }

        public async Task<DeviceReading> GetTodaysConsumption(CancellationToken cancellationToken)
        {
            if (_cancellationToken == default(CancellationToken))
            {
                _cancellationToken = cancellationToken;
            }
            _initSync.WaitOne();
            if (!_isConnected)
            {
                return null;
            }

            try
            {
                return await InternalDailyGet();
            }
            catch (Exception error)
            {
                Logger.RaiseErrorOnGettingData(error, this);
                _isConnected = false;
                Task.Factory.StartNew(ReConnect, cancellationToken);
            }

            return null;
        }

        protected abstract Task EnsureMethod();

        protected abstract Task<DeviceReading> InternalInstantGet();

        protected abstract Task<DeviceReading> InternalDailyGet();

        protected virtual void Dispose(bool disposing)
        {
            _initSync.Dispose();
        }

        protected async Task EnsureDevice()
        {
            try
            {
                await EnsureMethod();
                Logger.InformDeviceHasConnected(this);
                _isConnected = true;
            }
            catch (Exception error)
            {
                if (_isConnected)
                {
                    Logger.RaiseErrorOnDeviceConnections(error, this);
                    Task.Factory.StartNew(ReConnect, _cancellationToken);
                }
                _isConnected = false;
            }
            finally
            {
                _initSync.Set();
            }
        }

        private async Task ReConnect()
        {
            lock (_locker)
            {
                if (_reconnecting)
                {
                    return;
                }
                _reconnecting = true;
            }

            while (!_isConnected && !_cancellationToken.IsCancellationRequested)
            {
                await EnsureDevice();
                await Task.Delay(2000, _cancellationToken);
            }
            _reconnecting = false;
        }
    }
}
