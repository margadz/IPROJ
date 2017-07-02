using System;

namespace IPROJ_TcpCommunication
{
    public class TcpHost
    {
        private ushort _port;

        public TcpHost(string hostName, ushort port)
        {
            HostName = hostName;
            _port = port;
        }

        public TcpHost(string host)
        {
            ParseHostString(host);
        }

        public string HostName { get; private set; }

        public ushort Port
        {
            get
            {
                return _port;
            }
        }

        private void ParseHostString(string host)
        {
            var split = host.Split(':');
            if (split.Length != 2)
            {
                throw new ArgumentOutOfRangeException();
            }

            HostName = split[0];

            if (!ushort.TryParse(split[1], out _port))
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}