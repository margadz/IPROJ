using System;
using System.Threading.Tasks;
using IPROJ.ConnectionBroker.TcpCommunication;

namespace IPROJ.ConnectionBroker.DevicesManager.HS110
{
    public class HS110TcpConnector : IDisposable
    {
        private TcpConnector _connector;
        private bool _disposed;

        public HS110TcpConnector(TcpConnector connector)
        {
            _connector = connector;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        public async Task<string> QueryDevice(string command)
        {
            var response = await _connector.CallTcp(HS110Coding.Encrypt(command));

            return HS110Coding.Decrypt(response);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _connector?.Dispose();
            }

            _disposed = true;
        }
    }
}
