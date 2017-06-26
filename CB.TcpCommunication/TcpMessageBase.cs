using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IPROJ_TcpCommunication
{
    public class TcpMessageBase : IDisposable
    {
        private string _host;
        private ushort _port;
        private bool _disposed = false;
        private Socket _socket;

        public TcpMessageBase(string host, ushort port)
        {
            _host = host;
            _port = port;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Connect();
        }

        public byte[] CallTcp(byte[] message)
        {
            

            PutSync(message);
            return TakeSync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        private void Connect()
        {
            if (!_socket.Connected)
            {
                _socket.Connect(_host, _port);
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

        private void PutSync (byte [] message)
        {
            _socket.Send(message);
        }

        private byte [] TakeSync()
        {
            byte[] buffer = new byte[4096];

            int lent = _socket.Receive(buffer);

            return buffer.Take(lent).ToArray();
        }

        public static byte[] Encrypt(string payload, bool hasHeader = true)
        {
            byte key = 0xAB;
            byte[] cipherBytes = new byte[payload.Length];
            byte[] header = hasHeader ? BitConverter.GetBytes(ReverseBytes((UInt32)payload.Length)) : new byte[] { };
            for (var i = 0; i < payload.Length; i++)
            {
                cipherBytes[i] = Convert.ToByte(payload[i] ^ key);
                key = cipherBytes[i];
            }
            return header.Concat(cipherBytes).ToArray();
        }

        public static string Decrypt(byte[] cipher, bool hasHeader = true)
        {
            byte key = 0xAB;
            byte nextKey;
            if (hasHeader)
                cipher = cipher.Skip(4).ToArray();
            byte[] result = new byte[cipher.Length];

            for (int i = 0; i < cipher.Length; i++)
            {
                nextKey = cipher[i];
                result[i] = (byte)(cipher[i] ^ key);
                key = nextKey;
            }
            return Encoding.UTF7.GetString(result);
        }

        private static UInt32 ReverseBytes(UInt32 value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }
    }
}
