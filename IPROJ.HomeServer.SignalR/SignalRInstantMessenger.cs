using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;
using IPROJ.Contracts.Messaging;
using IPROJ.Contracts.Threading;
using Microsoft.AspNetCore.SignalR.Client;

namespace IPROJ.HomeServer.SignalR
{
    public class SignalRInstantMessenger : IInstantMessenger
    {
        private readonly HubConnection _hubConnection;
        private readonly IInstantMessengerLog _logger;
        private readonly ManualResetEvent _initSync = new ManualResetEvent(false);
        private bool _isConnected;
        private CancellationToken _cancellationToken;

        public SignalRInstantMessenger(IInstantMessengerLog logger, IThreadingInfrastructure threadingInfrastructure = null)
        {
            Argument.OfWichValueShoulBeProvided(logger, nameof(logger));

            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://192.168.0.108:12345/current")
                .Build();

            _logger = logger;
            _cancellationToken = threadingInfrastructure?.CancellationToken ?? CancellationToken.None;
            Task.Factory.StartNew(Initialize);
        }

        public void Dispose()
        {
            _hubConnection.StopAsync().Wait();
            _hubConnection.DisposeAsync().Wait();
            _initSync.Dispose();
        }

        public async Task SendMessage(IEnumerable<DeviceReading> deviceReadings, CancellationToken cancellationToken)
        {
            _initSync.WaitOne();
            if (!deviceReadings.Any())
            {
                return;
            }

            if (_isConnected)
            {
                var messages = deviceReadings.Where(reading => reading != null && reading.ReadingCharacter == ReadingCharacter.Instant);
                try
                {
                    await _hubConnection.InvokeAsync("SendMessage", messages, cancellationToken);
                }
                catch (Exception error)
                {
                    _isConnected = false;
                    _logger.RaiseDispatcherHubConnectionError(error);
                    Task.Factory.StartNew(() => TryToConnect(), _cancellationToken);
                }
            }
        }

        private async Task Initialize()
        {
            try
            {
                _logger.InformDispacherIsConnectingToHub();
                await _hubConnection.StartAsync();
                _isConnected = true;
                _logger.InformDispatcherConnectedToHub();
            }
            catch (Exception error)
            {
                _isConnected = false;
                _logger.RaiseDispatcherHubConnectionError(error);
                Task.Factory.StartNew(() => TryToConnect(), _cancellationToken);
            }
            finally
            {
                _initSync.Set();
            }
        }

        private async Task TryToConnect()
        {
            while (!_cancellationToken.IsCancellationRequested && !_isConnected)
            {
                try
                {
                    await _hubConnection.StartAsync();
                    _isConnected = true;
                    _logger.InformDispatcherConnectedToHub();
                }
                catch (Exception)
                {
                    //supress
                }
                await Task.Delay(2000, _cancellationToken);
            }
        }
    }
}
