using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace IPROJ.ConnectionBroker.TcpCommunication
{
    public class TcpConnector : IDisposable
    {
        private readonly AutoResetEvent _resetEvent = new AutoResetEvent(true);
        private readonly ManualResetEvent _initResetEvent = new ManualResetEvent(false);
        private readonly TcpHost _host;
        private Socket _socket;
        private bool _disposed = false;

        public TcpConnector(TcpHost host)
        {
            _host = host;
        }

        public async Task Put(byte[] message)
        {
            await EnsureConnected();
            await _socket.SendAsync(new ArraySegment<byte>(message), SocketFlags.None);
        }

        public async Task<byte[]> Take()
        {
            await EnsureConnected();
            byte[] buffer = new byte[4096];

            int length = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

            return buffer.Take(length).ToArray();
        }

        public virtual async Task<byte[]> CallTcp(byte[] message)
        {
            _resetEvent.WaitOne();
            try
            {
                await Put(message);
                var res = await Take();
                _socket.Dispose();
                return res;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _resetEvent.Set();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        private async Task EnsureConnected()
        {
            if (_socket == null || !_socket.Connected)
            {
                _socket?.Dispose();
                await Connect();
            }
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _socket?.Dispose();
            }

            _disposed = true;
        }

        private async Task Connect()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await _socket.ConnectAsync(_host.HostName, _host.Port);
        }
    }
}