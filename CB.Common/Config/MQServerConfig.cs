using System.Text;

namespace CB.Common.Config
{
    public static class MQServerConfig
    {
        public const string IP = "192.168.1.10";
        public const int PORT = 5672;
        public const string USER = "admin";
        public const string PASS = "kaszanka";
        public const string VHOST = "/";
        public const string READINGEXCHANGE = "Broker_Readings_Out";
        public static Encoding ENCODING = Encoding.Unicode;
    }
}
