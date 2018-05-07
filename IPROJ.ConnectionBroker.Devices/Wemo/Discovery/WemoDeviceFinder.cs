using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using IPROJ.Contracts.DataModel;
using IPROJ.Contracts.Device.Discovery;

namespace IPROJ.ConnectionBroker.Devices.Wemo.Discovery
{
    /// <summary>Discovers Wemo devices.</summary>
    public class WemoDeviceFinder : IDeviceFinder
    {
        private const string SearchString = "M-SEARCH * HTTP/1.1\r\nHOST:239.255.255.250:1900\r\nST:upnp:rootdevice\r\nMX:2\r\nMAN:\"ssdp:discover\"\r\n\r\n";
        private const string RegexString = @"((([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]):[0-9]{1,5})\/setup.xml.*Insight";
        private const int BroadCastPort = 1900;
        private const string Broadcast = "239.255.255.250";
        private const string Adress = "192.168.1.10";
        private const int Port = 60000;
        private static readonly IPEndPoint _localEndPoint = new IPEndPoint(IPAddress.Parse(Adress), Port);
        private static readonly IPEndPoint _multicastEndPoint = new IPEndPoint(IPAddress.Parse(Broadcast), BroadCastPort);
        private static readonly byte[] _payload = Encoding.UTF8.GetBytes(SearchString);
        private static readonly Regex Regex = new Regex(RegexString, RegexOptions.Singleline);

        /// <inheritdoc />
        public async Task<IEnumerable<DeviceDescription>> Discover(CancellationToken cancellationToken)
        {
            return ParseResult(await MakeACall());
        }

        private static IEnumerable<DeviceDescription> ParseResult(string callResult)
        {
            var result = new List<DeviceDescription>();

            var match = Regex.Match(callResult);
            while (match.Success)
            {
                result
                    .Add(new DeviceDescription() { Host = match.Groups[1].ToString(), IsActive = true, TypeOfDevice = DeviceType.WEMO, TypeOfReading = ReadingType.PowerConsumption });
                match = match.NextMatch();
            }

            return result;
        }

        private static async Task<string> MakeACall()
        {
            var buffer = new byte[8192];

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Bind(_localEndPoint);
                await socket.SendToAsync(new ArraySegment<byte>(_payload), SocketFlags.None, _multicastEndPoint);
                using (var source = new CancellationTokenSource(400))
                {
                    var offset = 0;
                    while (!source.Token.IsCancellationRequested)
                    {
                        if (socket.Available > 0)
                        {
                            offset += await socket.ReceiveAsync(new ArraySegment<byte>(buffer, offset, buffer.Length - offset), SocketFlags.None);
                        }
                    }
                }

                return Encoding.UTF8.GetString(buffer.ToArray());
            }
        }
    }
}
