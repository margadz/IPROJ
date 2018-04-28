using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;
using IPROJ.Contracts.Messaging;
using Microsoft.AspNetCore.SignalR.Client;

namespace IPROJ.HomeServer.SignalR
{
    public class SignalingDispatcher : ISignalingDispatcher
    {
        private readonly IQueueListener _queueListener;
        private readonly HubConnection _hubConnection;
        private readonly ISignalingDispatcherLog _logger;
        private readonly ManualResetEvent _initSync = new ManualResetEvent(false);
        private bool _isConnected;
        private CancellationToken _cancellationToken;

        public SignalingDispatcher(IQueueListener queueListener, ISignalingDispatcherLog logger)
        {
            Argument.OfWichValueShoulBeProvided(queueListener, nameof(queueListener));
            Argument.OfWichValueShoulBeProvided(logger, nameof(logger));

            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://192.168.1.10:12345/current")
                .Build();

            _queueListener = queueListener;
            _queueListener.QueueEvent += SendMessage;
            _logger = logger;
        }

        public void StartDispatching(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            Task.Factory.StartNew(Initialize);
            _initSync.WaitOne();
            _queueListener.Listen(cancellationToken);
        }

        public void Dispose()
        {
            _hubConnection.StopAsync().Wait();
            _hubConnection.DisposeAsync().Wait();
            _initSync.Dispose();
        }

        private async void SendMessage(IEnumerable<DeviceReading> deviceReadings)
        {
            if (_isConnected)
            {
                var messages = deviceReadings.Where(reading => reading != null && reading.ReadingCharacter == ReadingCharacter.Instant);
                try
                {
                    await _hubConnection.InvokeAsync("SendMessage", messages);
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
