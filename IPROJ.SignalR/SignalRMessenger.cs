using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Configuration.Configurations;
using IPROJ.Contracts.ConfigurationProvider;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Helpers;
using IPROJ.Contracts.Logging;
using IPROJ.Contracts.Messaging;
using IPROJ.Contracts.Threading;
using Microsoft.AspNetCore.SignalR.Client;

namespace IPROJ.SignalR.SignalR
{
    /// <summary>SingalR messenger.</summary>
    public class SignalRMessenger : IMessenger
    {
        private readonly HubConnection _hubConnection;
        private readonly IInstantMessengerLogger _logger;
        private readonly ManualResetEvent _initSync = new ManualResetEvent(false);
        private bool _isConnected;
        private CancellationToken _cancellationToken;

        /// <inheritdoc />
        public event EventHandler OnDeviceDiscoveryRequest;

        /// <inheritdoc />
        public event EventHandler<DeviceReading> OnStateSetChangeRequest;

        /// <summary>Initializes new instance of <see cref="SignalRMessenger"/>.</summary>
        /// <param name="logger">Logger.</param>
        /// <param name="configurationProvider">Configuration provider.</param>
        /// <param name="threadingInfrastructure">Threading infrastructure.</param>
        public SignalRMessenger(IInstantMessengerLogger logger, IConfigurationProvider configurationProvider, IThreadingInfrastructure threadingInfrastructure = null)
        {
            Argument.OfWichValueShoulBeProvided(logger, nameof(logger));
            Argument.OfWichValueShoulBeProvided(configurationProvider, nameof(configurationProvider));

            var baseUrl = configurationProvider.GetOption(CoreConfigurations.Category, CoreConfigurations.BaseAddress);
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"http://{baseUrl}:12345/current")
                .Build();

            OnDeviceDiscoveryRequest += DummyDiscoveryHandler;
            OnStateSetChangeRequest += DummyStateSetHanlder;
            _hubConnection.On("DiscoverDevicesRequest", () => OnDeviceDiscoveryRequest.Invoke(this, null));
            _hubConnection.On<DeviceReading>("SetDeviceStateRequest", (reading) => OnStateSetChangeRequest.Invoke(this, reading));
            _logger = logger;
            _cancellationToken = threadingInfrastructure?.CancellationToken ?? CancellationToken.None;
            Task.Run(Initialize);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _hubConnection.StopAsync().Wait();
            _hubConnection.DisposeAsync().Wait();
            _initSync.Dispose();
        }

        /// <inheritdoc />
        public async Task SendNewDevices(IEnumerable<DeviceDescription> newDevices, CancellationToken cancellationToken)
        {
            _initSync.WaitOne();

            await Send("SendDiscoveredDevices", newDevices, cancellationToken);
        }

        /// <inheritdoc />
        public async Task SendReadings(IEnumerable<DeviceReading> deviceReadings, CancellationToken cancellationToken)
        {

            _initSync.WaitOne();
            if (!deviceReadings.Any())
            {
                return;
            }

            var messages = deviceReadings.Where(reading => reading != null && reading.ReadingCharacter == ReadingCharacter.Instant);

            await Send("SendReadings", messages, cancellationToken);
        }

        private async Task Send(string methodName, object value, CancellationToken cancellationToken)
        {
            _initSync.WaitOne();
            if (_isConnected)
            {
                try
                {
                    await _hubConnection.InvokeAsync(methodName, value, cancellationToken);
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
                await _hubConnection.InvokeAsync("DiscoverDevicesRequest");
                await _hubConnection.InvokeAsync("SetDeviceStateRequest", new DeviceReading());
                _logger.InformDispatcherConnectedToHub();
            }
            catch (Exception error)
            {
                _isConnected = false;
                _logger.RaiseDispatcherHubConnectionError(error);
                Task.Run(() => TryToConnect(), _cancellationToken);
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
                    //Supress. Connection cannot be established.
                }
                await Task.Delay(2000, _cancellationToken);
            }
        }

        private void DummyDiscoveryHandler(object sender, EventArgs args)
        {
        }

        private void DummyStateSetHanlder(object sender, DeviceReading args)
        {
        }
    }
}
