using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Messaging;
using Microsoft.AspNetCore.SignalR.Client;

namespace IPROJ.SignalR
{
    public class SignalingDispatcher : ISignalingDispatcher
    {
        private readonly IQueueListener _queueListener;
        private readonly HubConnection _hubConnection;
        private readonly ManualResetEvent _initSync = new ManualResetEvent(false);
        private bool _isConnected;


        public SignalingDispatcher(IQueueListener queueListener)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://192.168.1.10:12345/current")
                .Build();

            _queueListener = queueListener;
            _queueListener.QueueEvent += SendMessage;
            Task.Factory.StartNew(Initialize);
        }

        public void StartDispatching(CancellationToken cancellationToken)
        {
            if (_isConnected)
            {
                _initSync.WaitOne();
                _queueListener.Listen(cancellationToken);
            }
        }

        public void Dispose()
        {
            _hubConnection.StopAsync().Wait();
            _hubConnection.DisposeAsync().Wait();
            _initSync.Dispose();
        }

        private async void SendMessage(IEnumerable<DeviceReading> deviceReadings)
        {
            var messages = deviceReadings.Where(reading => reading != null && reading.ReadingCharacter == ReadingCharacter.Instant);
            await _hubConnection.InvokeAsync("SendMessage", messages);
        }

        private async Task Initialize()
        {
            try
            {
                await _hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not connect to hub: {ex.Message}");
            }
            finally
            {
                _initSync.Set();
            }
            Console.WriteLine($"Connected to hub.");
            _isConnected = true;
        }
    }
}
