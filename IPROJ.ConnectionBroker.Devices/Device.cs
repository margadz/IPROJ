﻿using System;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;

namespace IPROJ.ConnectionBroker.Devices
{
    public abstract class Device : IDevice
    {
        private readonly ManualResetEvent _initSync = new ManualResetEvent(false);
        private object _locker = new object();
        private bool _isDisposed;
        private bool _isConnected;
        private bool _reconnecting;

        public Device(IDeviceLog logger)
        {
            Argument.OfWichValueShoulBeProvided(logger, nameof(logger));
            Logger = logger;
        }

        protected IDeviceLog Logger { get; }

        public abstract Guid DeviceId { get; }

        public abstract string DeviceName { get; }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                _isDisposed = true;
            }
        }


        public async Task<DeviceReading> GetInsantReading()
        {
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
                Task.Factory.StartNew(ReConnect);
            }

            return null;
        }

        public async Task<DeviceReading> GetTodaysConsumption()
        {
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
                Task.Factory.StartNew(ReConnect);
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
                }
                _isConnected = false;
                Task.Factory.StartNew(ReConnect);
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
                _reconnecting = true;
            }

            if (_reconnecting)
            {
                return;
            }

            while (!_isConnected)
            {
                await EnsureDevice();
                await Task.Delay(2000);
            }
            _reconnecting = false;
        }
    }
}
