using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace IPROJ_TcpCommunication
{
    public class TcpConnector : IDisposable
    {
        private readonly Socket _socket;
        private readonly TcpHost _host;
        private bool _disposed = false;

        public TcpConnector(TcpHost host)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _host = host;
            Connect().Wait();
        }

        public async Task Send(byte[] message)
        {
            await _socket.SendAsync(new ArraySegment<byte>(message), SocketFlags.None);
        }

        public async Task<byte[]> Take()
        {
            byte[] buffer = new byte[4096];

            int lent = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

            return buffer.Take(lent).ToArray();
        }

        public async Task<byte[]> CallTcp(byte[] message)
        {
            await Send(message);
            return await Take();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
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
            if (!_socket.Connected)
            {
                await _socket.ConnectAsync(_host.HostName, _host.Port);
            }
        }
    }
}