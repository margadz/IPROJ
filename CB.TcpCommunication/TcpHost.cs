namespace IPROJ_TcpCommunication
{
    public class TcpHost
    {
        public TcpHost(string hostName, ushort port)
        {
            HostName = hostName;
            Port = port;
        }

        public string HostName { get; }

        public ushort Port { get; }
    }
}