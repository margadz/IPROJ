using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Common.ConfigData
{
    public static class MQServerConfig
    {
        public const string IP = "192.168.1.10";
        public const int PORT = 5672;
        public const string USER = "homeServer";
        public const string PASS = "szczawzmiodem";
        public const string VHOST = "/";
        public const string ReadingsQueue = "HomeServerReadings";
        public static Encoding ENCODING = Encoding.Unicode;
    }
}
