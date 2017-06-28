namespace IPROJ.Core.Net
{
    public class Host
    {
        public Host(string hostName, ushort port)
        { 
            HostName = hostName;
            Port = port;
        }

        public string HostName { get; }
        public ushort Port { get; }
    }
}
